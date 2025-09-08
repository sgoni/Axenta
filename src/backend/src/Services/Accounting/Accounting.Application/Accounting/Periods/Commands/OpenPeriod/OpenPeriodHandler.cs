namespace Accounting.Application.Accounting.Periods.Commands.OpenPeriod;

public class OpenPeriodHandler(IApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<OpenPeriodCommand, OpenPeriodResult>
{
    public async Task<OpenPeriodResult> Handle(OpenPeriodCommand command, CancellationToken cancellationToken)
    {
        // 1) I confirm the domain transaction
        // 2) Public the event (if it fails, MT reimburses according to politics)
        // return result

        // Check company
        var companyId = CompanyId.Of(command.Period.CompanyId);
        var company = await dbContext.Companies.FindAsync(companyId, cancellationToken);

        if (company is null || company is null)
            throw EntityNotFoundException.For<Company>(command.Period.CompanyId);

        //Validation accounting period is Open
        await PeriodIsOpen(command.Period.PeriodId, cancellationToken);

        // Domain rule (does not persist, does not touch infra)
        //period.Reopen(Array.Empty<JournalEntry>());
        //await dbContext.SaveChangesAsync(cancellationToken);

        var eventMessage = command.Period.Adapt<PeriodReopenedIntegrationEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new OpenPeriodResult(true);
    }

    private async Task PeriodIsOpen(Guid id, CancellationToken cancellationToken)
    {
        var periodId = PeriodId.Of(id);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null) throw EntityNotFoundException.For<Period>(id);

        if (!period.IsClosed)
            throw new Exception($"The period id: {id} is now opened.");
    }
}