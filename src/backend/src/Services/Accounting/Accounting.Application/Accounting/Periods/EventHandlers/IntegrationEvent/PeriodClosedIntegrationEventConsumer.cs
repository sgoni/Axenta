namespace Accounting.Application.Accounting.Periods.EventHandlers.IntegrationEvent;

public class PeriodClosedIntegrationEventConsumer(
    IApplicationDbContext dbContext,
    IEventLogRepository eventLogRepository,
    IJournalEntryRepository journalEntryRepository,
    IPublishEndpoint publishEndpoint,
    ISender sender,
    ILogger<PeriodClosedIntegrationEventConsumer> logger) : IConsumer<PeriodClosedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PeriodClosedIntegrationEvent> context)
    {
        var @event = context.Message;

        // --- Idempotence --- 
        // Avoid processing the same event twice (if the rententred broker). 
        // Options: 
        // 1) keep a "eventlog" with Messageid (context.messageid) processed. 
        if (await eventLogRepository.AlreadyProcessedAsync(@event.PeriodId))
            // We already process it → Ignore
            return;

        // 2) Verify if there are already the reversals of the period.
        if (await journalEntryRepository.PeriodAlreadyClosedAsync(@event.PeriodId))
            // There is already reverse → Ignore
            return;

        await CheckCompanyExistence(@event); // Check company

        // To obtain the opening entries of the infrastructure
        var openingEntries = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.PeriodId == PeriodId.Of(@event.PeriodId))
            .ToListAsync();

        // Closing the opening entries
        foreach (var entry in openingEntries.Where(e => e.JournalEntryType.Equals(JournalEntryType.Normal.Name)))
        {
            var commandEntry = MapToJournalEntryDto(entry);
            await sender.Send(commandEntry);
        }

        // Create accounts for defect of the countable cierre
        //var command = PeriodClosureSeat(context.Message);
        //await sender.Send(command);

        // Save Period in DB
        var period = await ValidateAndClosePeriod(@event); // Check period
        await dbContext.SaveChangesAsync(default);

        // We keep in the log that this message was attended
        await eventLogRepository.SaveProcessedAsync(@event.PeriodId);

        var emailEvent = new EmailNotificationIntegrationEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "finanzas@empresa.com", // TODO: configurable
            "Closing accounting period",
            $"The period {@event.Year}-{@event.Month} has been closed by {@event.ClosedBy}."
        );

        await publishEndpoint.Publish(emailEvent);

        // Redelivery with Delay due to external dependence not available:
        // throw new DelayedRedeliveryException(TimeSpan.FromSeconds(30));
    }

    private async Task CheckCompanyExistence(PeriodClosedIntegrationEvent @event)
    {
        // Check company
        var companyId = CompanyId.Of(@event.CompanyId);
        var company = await dbContext.Companies.FindAsync([companyId], default);

        if (company is null) throw EntityNotFoundException.For<Company>(@event.CompanyId);
    }

    private async Task<Period> ValidateAndClosePeriod(PeriodClosedIntegrationEvent @event)
    {
        // Check period
        var periodId = PeriodId.Of(@event.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId).ConfigureAwait(false);

        if (period is null) throw EntityNotFoundException.For<Period>(@event.PeriodId);

        // Domain rule (does not persist, does not touch infra)
        period.Close();

        return period;
    }

    private UpdateJournalEntryCommand MapToJournalEntryDto(JournalEntry message)
    {
        var journalEntryDto = new JournalEntryDto(
            message.Id.Value,
            message.Date,
            message.Description,
            message.PeriodId.Value,
            message.CompanyId.Value,
            message.CurrencyCode,
            message.ExchangeRate,
            message.ExchangeRateDate,
            JournalEntryType.Closing.Name,
            message.ReversalJournalEntryId?.Value,
            message.JournalEntryLines
                .Select(ln => new JournalEntryLineDto(ln.Id.Value, ln.JournalEntryId.Value,
                    ln.AccountId.Value, ln.Debit.Amount, ln.Credit.Amount, ln.CostCenterId?.Value, ln.LineNumber))
                .ToList()
        );

        return new UpdateJournalEntryCommand(journalEntryDto);
    }

    private CreateJournalEntryCommand PeriodClosureSeat(PeriodClosedIntegrationEvent message,
        Company company = null)
    {
        var journalEntryId = Guid.NewGuid();

        var journalEntryDto = new JournalEntryDto(
            journalEntryId,
            DateTime.UtcNow,
            $"Period closing entry {DateTime.Now.Year}-{DateTime.Today.Month}",
            message.PeriodId,
            message.CompanyId,
            company.CurrencyCode,
            0,
            DateOnly.FromDateTime(DateTime.UtcNow),
            JournalEntryType.Closing.Name,
            null,
            null
        );

        return new CreateJournalEntryCommand(journalEntryDto);
    }
}