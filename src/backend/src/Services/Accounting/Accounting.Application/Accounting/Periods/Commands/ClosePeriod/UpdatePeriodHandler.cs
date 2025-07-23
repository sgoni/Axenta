namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public class UpdatePeriodHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ClosePeriodCommand, ClosePeriodResult>
{
    public async Task<ClosePeriodResult> Handle(ClosePeriodCommand command, CancellationToken cancellationToken)
    {
        //Update period entity from command object
        //save to database
        //return result

        var periodId = PeriodId.Of(command.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period == null) throw new PeriodNotFoundException(command.PeriodId);

        period.Close();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ClosePeriodResult(true);
    }
}