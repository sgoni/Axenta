using Axenta.BuildingBlocks.ValueObjects;

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
            .FirstOrDefaultAsync(cancellationToken);

        if (journalEntry is null)
            throw EntityNotFoundException.For<JournalEntry>(command.JournalEntry.Id);

        // Validation accounting period is Open
        await PeriodIsOpen(command, cancellationToken);

        // JournalEntry is  reversed
        if (journalEntry.JournalEntryType.Equals(JournalEntryType.Reversal.Name))
            throw new BadRequestException("The journal entry currently appears reversed.");

        journalEntry.Update(
            command.JournalEntry.Description,
            command.JournalEntry.Date,
            command.JournalEntry.CurrencyCode,
            command.JournalEntry.ExchangeRate,
            command.JournalEntry.ExchangeRateDate,
            command.JournalEntry.JournalEntryType
        );

        dbContext.JournalEntries.Update(journalEntry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateJournalEntryResult(true);
    }

    private async Task PeriodIsOpen(UpdateJournalEntryCommand command, CancellationToken cancellationToken)
    {
        var periodId = PeriodId.Of(command.JournalEntry.PeriodId);
        var period = await dbContext.Periods.FindAsync(periodId, cancellationToken);

        if (period is null) throw EntityNotFoundException.For<Period>(command.JournalEntry.PeriodId);

        if (period.IsClosed)
            throw new BadRequestException("The accounting period is closed and seats cannot be modified.");
    }

    private JournalEntry CreateNewJournalEntry(JournalEntryDto journalEntryDto)
    {
        var lineNumber = 1;

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
        foreach (var journalEntryLineDto in journalEntryDto.Lines)
            newJournalEntry.AddLine(
                AccountId.Of(journalEntryLineDto.AccountId),
                Money.Of(journalEntryLineDto.Debit, journalEntryDto.CurrencyCode!),
                Money.Of(journalEntryLineDto.Credit, journalEntryDto.CurrencyCode!),
                CostCenterId.FromNullable(journalEntryLineDto.CostCenterId),
                lineNumber++
            );

        //Accounting validation Must = Have
        newJournalEntry.ValidateBalance();

        //Add document references

        return newJournalEntry;
    }
}