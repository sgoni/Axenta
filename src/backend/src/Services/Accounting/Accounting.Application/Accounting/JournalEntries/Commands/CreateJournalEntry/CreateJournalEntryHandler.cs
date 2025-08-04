using ValidationException = FluentValidation.ValidationException;

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

        //Validation accounting period is Open
        var periodId = PeriodId.Of(command.JournalEntry.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null) throw new PeriodNotFoundException(command.JournalEntry.PeriodId);

        if (period.IsClosed)
            throw new ValidationException("The accounting period is closed and seats cannot be registered.");

        var journalEntry = CreateNewJournalEntry(command.JournalEntry);

        dbContext.JournalEntries.Add(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateJournalEntryResult(journalEntry.Id.Value);
    }

    private JournalEntry CreateNewJournalEntry(JournalEntryDto journalEntryDto)
    {
        var lineNumber = 1;

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
                lineNumber++
            );

        //Accounting validation Must = Have
        newJournalEntry.ValidateBalance();

        //Add document references

        return newJournalEntry;
    }
}