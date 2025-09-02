using Axenta.BuildingBlocks.ValueObjects;

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
        await PeriodIsOpen(command, cancellationToken);

        var journalEntry = CreateNewJournalEntry(command.JournalEntry);

        dbContext.JournalEntries.Add(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateJournalEntryResult(journalEntry.Id.Value);
    }

    private async Task PeriodIsOpen(CreateJournalEntryCommand command, CancellationToken cancellationToken)
    {
        var periodId = PeriodId.Of(command.JournalEntry.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null) throw EntityNotFoundException.For<Period>(command.JournalEntry.PeriodId);

        if (period.IsClosed)
            throw new BadRequestException("The accounting period is closed and seats cannot be registered.");
    }

    private JournalEntry CreateNewJournalEntry(JournalEntryDto journalEntryDto)
    {
        //Create header
        var newJournalEntry = JournalEntry.Create(
            JournalEntryId.Of(Guid.NewGuid()),
            journalEntryDto.Date,
            journalEntryDto.Description,
            PeriodId.Of(journalEntryDto.PeriodId),
            CompanyId.Of(journalEntryDto.CompanyId),
            journalEntryDto.CurrencyCode,
            journalEntryDto.ExchangeRate,
            journalEntryDto.ExchangeRateDate
        );

        //Add details
        var lineNumber = 1;
        foreach (var journalEntryLineDto in journalEntryDto.Lines)
            newJournalEntry.AddLine(
                AccountId.Of(journalEntryLineDto.AccountId),
                Money.Of(journalEntryLineDto.Debit, journalEntryDto.CurrencyCode),
                Money.Of(journalEntryLineDto.Credit, journalEntryDto.CurrencyCode),
                lineNumber++
            );

        //Accounting validation Must = Have
        newJournalEntry.ValidateBalance();

        //Add document references

        return newJournalEntry;
    }
}