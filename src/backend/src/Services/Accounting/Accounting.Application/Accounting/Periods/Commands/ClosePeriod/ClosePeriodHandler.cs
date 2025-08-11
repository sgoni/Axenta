namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public class ClosePeriodHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ClosePeriodCommand, ClosePeriodResult>
{
    public async Task<ClosePeriodResult> Handle(ClosePeriodCommand command, CancellationToken cancellationToken)
    {
        //Update period entity from command object
        //save to database
        //return result

        // Check company
        var companyId = CompanyId.Of(command.ClosePeriod.CompanyId);
        var company = await dbContext.Companies.FindAsync(companyId, cancellationToken);

        if (company is null || company is null)
            throw new CompanyNotFoundException(command.ClosePeriod.CompanyId);

        // Check period
        var periodId = PeriodId.Of(command.ClosePeriod.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null)
            throw new PeriodNotFoundException(command.ClosePeriod.PeriodId);

        if (period.IsClosed)
            throw new Exception($"The period id: {command.ClosePeriod.PeriodId} is now closed.");

        // 1 Generate closing entry (simplified example)
        var closingEntry = JournalEntry.Create(
            JournalEntryId.Of(Guid.NewGuid()),
            DateTime.UtcNow,
            $"Period closing entry {period.Year}-{period.Month}",
            periodId,
            companyId,
            company.CurrencyCode,
            0,
            null
        );

        // Here we would use templates (point 2) to define the lines

        period.Close();
        dbContext.JournalEntries.Add(closingEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ClosePeriodResult(true);
    }
}