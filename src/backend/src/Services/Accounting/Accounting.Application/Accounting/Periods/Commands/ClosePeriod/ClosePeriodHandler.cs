namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public class ClosePeriodHandler(IApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<ClosePeriodCommand, ClosePeriodResult>
{
    public async Task<ClosePeriodResult> Handle(ClosePeriodCommand command, CancellationToken cancellationToken)
    {
        //Update period entity from command object
        //save to database
        //return result

        // Check company
        var companyId = CompanyId.Of(command.Period.CompanyId);
        var company = await dbContext.Companies.FindAsync(companyId, cancellationToken);

        if (company is null || company is null)
            throw EntityNotFoundException.For<Company>(command.Period.CompanyId);

        // Check period
        await PeriodIsClose(command.Period.PeriodId, cancellationToken);

        var eventMessage = command.Period.Adapt<PeriodClosedIntegrationEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // Domain rule (does not persist, does not touch infra)
        //period.Close();
        //await dbContext.SaveChangesAsync(cancellationToken);

        return new ClosePeriodResult(true);  
    }

    private async Task PeriodIsClose(Guid id, CancellationToken cancellationToken)
    {
        var periodId = PeriodId.Of(id);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null) throw EntityNotFoundException.For<Period>(id);

        if (period.IsClosed)
            throw new Exception($"The period id: {id} is now closed.");
    }
}