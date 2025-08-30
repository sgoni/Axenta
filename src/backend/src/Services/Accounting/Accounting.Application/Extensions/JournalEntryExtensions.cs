namespace Accounting.Application.Extensions;

public static class JournalEntryExtensions
{
    public static IEnumerable<JournalEntryDto> ToJournalEntryDtoList(
        this IEnumerable<JournalEntry> journalEntries)
    {
        return journalEntries.Select(journalEntry => new JournalEntryDto(
            journalEntry.Id.Value,
            journalEntry.Date,
            journalEntry.Description,
            journalEntry.PeriodId.Value,
            journalEntry.CompanyId.Value,
            journalEntry.CurrencyCode,
            journalEntry.ExchangeRate,
            journalEntry.ExchangeRateDate,
            journalEntry.JournalEntryType,
            journalEntry.ReversalJournalEntryId.Value,
            journalEntry.JournalEntryLines
                .Select(ln => new JournalEntryLineDto(ln.Id.Value, ln.JournalEntryId.Value, ln.AccountId.Value,
                    ln.Debit, ln.Credit, ln.LineNumber)).ToList()
        ));
    }

    public static JournalEntryDto ToJournalEntryDto(this JournalEntry journalEntry)
    {
        return DtoFromJournalEntry(journalEntry);
    }

    public static JournalEntryDto DtoFromJournalEntry(JournalEntry journalEntry)
    {
        return new JournalEntryDto(
            journalEntry.Id.Value,
            journalEntry.Date,
            journalEntry.Description,
            journalEntry.PeriodId.Value,
            journalEntry.CompanyId.Value,
            journalEntry.CurrencyCode,
            journalEntry.ExchangeRate,
            journalEntry.ExchangeRateDate,
            journalEntry.JournalEntryType,
            journalEntry.ReversalJournalEntryId?.Value,
            journalEntry.JournalEntryLines
                .Select(ln => new JournalEntryLineDto(ln.Id.Value, ln.JournalEntryId.Value, ln.AccountId.Value,
                    ln.Debit, ln.Credit, ln.LineNumber)).ToList()
        );
    }
}