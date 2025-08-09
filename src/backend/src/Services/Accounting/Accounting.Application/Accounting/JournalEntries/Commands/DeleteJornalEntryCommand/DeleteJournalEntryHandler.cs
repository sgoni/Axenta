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

        journalEntry.CancelSeat();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteJournalEntrytResult(true);
    }
}