namespace Accounting.Application.Accounting.JournalEntries.Commands.ReverseJournalEntry;

public class ReverseJournalEntryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ReverseJournalEntryCommand, ReverseJournalEntryResult>
{
    public async Task<ReverseJournalEntryResult> Handle(ReverseJournalEntryCommand command,
        CancellationToken cancellationToken)
    {
        var journalEntry = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.Id == JournalEntryId.Of(command.ReversalJournalEntryId))
            .FirstOrDefaultAsync(cancellationToken);

        if (journalEntry is null)
            throw new JournalEntryNotFoundExceptions(command.ReversalJournalEntryId);

        //Validation accounting period is Open
        await PeriodIsOpen(journalEntry, cancellationToken);

        var reversal = journalEntry.Reverse();
        dbContext.JournalEntries.Add(reversal);
        dbContext.JournalEntries.Update(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ReverseJournalEntryResult(true);
    }

    private async Task PeriodIsOpen(JournalEntry journalEntry, CancellationToken cancellationToken)
    {
        var period = await dbContext.Periods.FindAsync(journalEntry.PeriodId, cancellationToken);

        if (period is null) throw new PeriodNotFoundException(period.Id.Value);

        if (period.IsClosed)
            throw new BadRequestException("The accounting period is closed, it cannot be reversed.");
    }
}