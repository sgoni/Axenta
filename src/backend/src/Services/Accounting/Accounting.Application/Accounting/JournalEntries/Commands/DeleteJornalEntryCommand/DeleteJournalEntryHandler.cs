namespace Accounting.Application.Accounting.JournalEntries.Commands.DeleteJornalEntryCommand;

public class DeleteJournalEntryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteJournalEntryCommand, DeleteJournalEntrytResult>
{
    public async Task<DeleteJournalEntrytResult> Handle(DeleteJournalEntryCommand command,
        CancellationToken cancellationToken)
    {
        //Delete JournalEntry entity from command object
        //save to database
        //return result

        var journalEntryId = JournalEntryId.Of(command.journalEntryId);
        var journalEntry = await dbContext.JournalEntries.FindAsync([journalEntryId], cancellationToken);

        if (journalEntry is null) throw new JournalEntryNotFoundExceptions(command.journalEntryId);

        // Validation accounting period is Open
        await PeriodIsOpen(journalEntry.PeriodId, cancellationToken);

        // JournalEntry is reversed
        if (journalEntry.JournalEntryType.Equals(JournalEntryType.Reversal.Name))
            throw new BadRequestException("The journal entry is now reversed.");

        // JournalEntry is close
        if (journalEntry.JournalEntryType.Equals(JournalEntryType.Closing.Name))
            throw new BadRequestException(
                "The journal entry appears verified and cannot be physically deleted, please proceed to reverse it.");

        dbContext.JournalEntries.Remove(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteJournalEntrytResult(true);
    }

    private async Task PeriodIsOpen(PeriodId id, CancellationToken cancellationToken)
    {
        var period = await dbContext.Periods.FindAsync(id, cancellationToken);

        if (period is null) throw new PeriodNotFoundException(id.Value);

        if (period.IsClosed)
            throw new BadRequestException("The accounting period is closed and seats cannot be modified.");
    }
}