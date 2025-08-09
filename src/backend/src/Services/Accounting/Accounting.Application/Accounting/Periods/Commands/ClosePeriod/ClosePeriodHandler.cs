namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public class ClosePeriodHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ClosePeriodCommand, ClosePeriodResult>
{
    public async Task<ClosePeriodResult> Handle(ClosePeriodCommand command, CancellationToken cancellationToken)
    {
        //Update period entity from command object
        //save to database
        //return result

        var periodId = PeriodId.Of(command.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null)
            throw new PeriodNotFoundException(command.PeriodId);

        if (period.IsClosed)
            throw new Exception($"The period id: {command.PeriodId} is now closed.");

        // 1 Generate closing entry (simplified example)
        var closingEntry = JournalEntry.Create(
            JournalEntryId.Of(Guid.NewGuid()),
            DateTime.UtcNow,
            $"Period closing entry {period.Year}-{period.Month}",
            period.Id
        );

        // Here we would use templates (point 2) to define the lines

        period.Close();
        dbContext.JournalEntries.Add(closingEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ClosePeriodResult(true);
    }
}