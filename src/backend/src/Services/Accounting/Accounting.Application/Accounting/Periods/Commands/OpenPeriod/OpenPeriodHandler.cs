namespace Accounting.Application.Accounting.Periods.Commands.OpenPeriod;

public class OpenPeriodHandler(IApplicationDbContext dbContext)
    : ICommandHandler<OpenPeriodCommand, OpenPeriodResult>
{
    public async Task<OpenPeriodResult> Handle(OpenPeriodCommand command, CancellationToken cancellationToken)
    {
        //Update period entity from command object
        //save to database
        //return result

        var periodId = PeriodId.Of(command.Period.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period == null) throw new PeriodNotFoundException(command.Period.PeriodId);

        period.Open();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new OpenPeriodResult(true);
    }
}