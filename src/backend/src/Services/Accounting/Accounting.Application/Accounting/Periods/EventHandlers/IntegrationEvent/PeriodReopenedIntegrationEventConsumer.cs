namespace Accounting.Application.Accounting.Periods.EventHandlers.IntegrationEvent;

public class PeriodReopenedIntegrationEventConsumer(
    IApplicationDbContext dbContext,
    IEventLogRepository eventLogRepository,
    IJournalEntryRepository journalEntryRepository,
    ISender sender,
    ILogger<PeriodReopenedIntegrationEventConsumer> logger) : IConsumer<PeriodReopenedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PeriodReopenedIntegrationEvent> context)
    {
        var @event = context.Message;

        // --- Idempotence --- 
        // Avoid processing the same event twice (if the rententred broker). 
        // Options: 
        // 1) keep a "eventlog" with Messageid (context.messageid) processed. 
        //if (await eventLogRepository.AlreadyProcessedAsync(@event.PeriodId))
        // We already process it → Ignore
        //    return;

        // 2) Verify if there are already the reversals of the period.
        if (await journalEntryRepository.PeriodAlreadyReversedAsync(@event.PeriodId))
            // There is already reverse → Ignore
            return;

        // We obtain the closing entries of the infrastructure
        var closingEntries = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.PeriodId == PeriodId.Of(@event.PeriodId))
            .ToListAsync();

        // Reverse the closing entries
        foreach (var entry in closingEntries.Where(e => e.JournalEntryType.Equals(JournalEntryType.Closing.Name)))
        {
            var command = MapToReverseJournalEntryCommand(entry.Id.Value);
            await sender.Send(command);
        }

        // Save Period in DB
        var period = await ValidateAndOpenPeriod(@event); // Check period
        await dbContext.SaveChangesAsync(default);

        // We keep in the log that this message was attended
        await eventLogRepository.SaveProcessedAsync(@event.PeriodId);

        // Redelivery with Delay due to external dependence not available:
        // throw new DelayedRedeliveryException(TimeSpan.FromSeconds(30));
    }

    private async Task<Period> ValidateAndOpenPeriod(PeriodReopenedIntegrationEvent @event)
    {
        // Check period
        var periodId = PeriodId.Of(@event.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId).ConfigureAwait(false);

        if (period is null) throw EntityNotFoundException.For<Period>(@event.PeriodId);

        // Domain rule (does not persist, does not touch infra)
        period.Reopen(Array.Empty<JournalEntry>());

        return period;
    }

    private ReverseJournalEntryCommand MapToReverseJournalEntryCommand(Guid message)
    {
        return new ReverseJournalEntryCommand(message);
    }
}