namespace Accounting.Application.Accounting.JournalEntries.Commands.CreateJournalEntry;

public class CreateJournalEntryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateJournalEntryCommand, CreateJournalEntryResult>
{
    public async Task<CreateJournalEntryResult> Handle(CreateJournalEntryCommand command,
        CancellationToken cancellationToken)
    {
        //create Journal Entry entity from command object
        //save to database
        //return result 

        var journalEntry = CreateNewJournalEntry(command.JournalEntry);

        dbContext.JournalEntries.Add(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateJournalEntryResult(journalEntry.Id.Value);
    }

    private JournalEntry CreateNewJournalEntry(JournalEntryDto journalEntryDto)
    {
        var LineNumber = 1;

        //Create header
        var newJournalEntry = JournalEntry.Create(
            JournalEntryId.Of(Guid.NewGuid()),
            journalEntryDto.Date,
            journalEntryDto.Description,
            PeriodId.Of(journalEntryDto.PeriodId)
        );

        //Add details
        foreach (var journalEntryLineDto in journalEntryDto.Lines)
            newJournalEntry.AddLine(
                AccountId.Of(journalEntryLineDto.AccountId),
                journalEntryLineDto.Debit,
                journalEntryLineDto.Credit,
                LineNumber++
            );

        //Add document references

        return newJournalEntry;
    }
}