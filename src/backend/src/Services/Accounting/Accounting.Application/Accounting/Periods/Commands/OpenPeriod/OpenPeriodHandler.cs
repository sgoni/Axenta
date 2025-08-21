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

        //Validation accounting period is Open
        await PeriodIsOpen(command.Period.PeriodId, cancellationToken);

        // We obtain the closing entries of the infrastructure
        var closingEntries = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.PeriodId == PeriodId.Of(command.Period.PeriodId))
            .ToListAsync(cancellationToken);

        // The revert logic is executed in the domain
        period.Reopen(closingEntries);
        
        // We persist returned reversals
        foreach (var entry in closingEntries.Where(e => e.IsPosted))
        {
            var reversal = entry.Reverse();
            dbContext.JournalEntries.Add(reversal);
            dbContext.JournalEntries.Update(entry);
        }
        
        //period.Open();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new OpenPeriodResult(true);
    }

    private async Task PeriodIsOpen(Guid id, CancellationToken cancellationToken)
    {
        var periodId = PeriodId.Of(id);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null) throw new PeriodNotFoundException(id);
        ;
    }
}