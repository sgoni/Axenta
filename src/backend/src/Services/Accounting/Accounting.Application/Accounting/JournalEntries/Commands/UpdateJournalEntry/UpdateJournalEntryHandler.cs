namespace Accounting.Application.Accounting.JournalEntries.Commands.UpdateJournalEntry;

public record UpdateJournalEntryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateJournalEntryCommand, UpdateJournalEntryResult>
{
    public async Task<UpdateJournalEntryResult> Handle(UpdateJournalEntryCommand command,
        CancellationToken cancellationToken)
    {
        //Update JournalEntry entity from command object
        //save to database
        //return result

        //Scenarios to contemplate when upgrading an accounting seat: 
        //1. Modification of general JournalEntry properties (date, description, etc.).\
        //2. Modification of existing line items (eg: account change, debit or credit).
        //3. Elimination of lines. 
        //4. Addition of new lines.
        //5. Balance sheet validation: total debit = total credit after applying changes.

        var journalEntry = await dbContext.JournalEntries
            .Include(je => je.JournalEntryLines)
            .AsNoTracking()
            .Where(je => je.Id == JournalEntryId.Of(command.JournalEntry.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (journalEntry is null)
            throw new JournalEntryNotFoundExceptions(command.JournalEntry.Id);

        UpdateOrderWithNewValues(journalEntry, command.JournalEntry);

        dbContext.JournalEntries.Update(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateJournalEntryResult(true);
    }

    private void UpdateOrderWithNewValues(JournalEntry journalEntry, JournalEntryDto commandJournalEntry)
    {
        journalEntry.Update(journalEntry.Description, journalEntry.Date, PeriodId.Of(journalEntry.PeriodId.Value));
    }
}