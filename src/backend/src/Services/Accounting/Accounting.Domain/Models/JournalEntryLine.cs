namespace Accounting.Domain.Models;

/// <summary>
///     Detail (double entry)
/// </summary>
public class JournalEntryLine : Entity<JournalEntryLineId>
{
    internal JournalEntryLine(JournalEntryId journalEntryId, AccountId accountId, decimal debit, decimal credit,
        int lineNumber)
    {
        Id = JournalEntryLineId.Of(Guid.NewGuid());
        JournalEntryId = journalEntryId;
        AccountId = accountId;
        Debit = debit;
        Credit = credit;
        LineNumber = lineNumber;
    }

    public JournalEntryId JournalEntryId { get; private set; } = default!;
    public AccountId AccountId { get; private set; } = default!;
    public decimal Debit { get; private set; }
    public decimal Credit { get; private set; }
    public int LineNumber { get; private set; }
}