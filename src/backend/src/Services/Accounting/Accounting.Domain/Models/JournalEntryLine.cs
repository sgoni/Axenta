namespace Accounting.Domain.Models;

/// <summary>
///     Detail (double entry)
/// </summary>
public class JournalEntryLine : Entity<JournalEntryLineId>
{
    private JournalEntryLine()
    {
    } // Necesario para EF

    internal JournalEntryLine(JournalEntryId journalEntryId, AccountId accountId, Money debit, Money credit,
        CostCenterId costCenterId, int lineNumber)
    {
        if (debit is null || credit is null) throw new ArgumentNullException();
        if (debit.CurrencyCode != credit.CurrencyCode)
            throw new DomainException("Debit and Credit must share the same currency.");

        Id = JournalEntryLineId.Of(Guid.NewGuid());
        JournalEntryId = journalEntryId;
        AccountId = accountId;
        Debit = debit;
        Credit = credit;
        LineNumber = lineNumber;
        CostCenterId = costCenterId;
    }

    public JournalEntryId JournalEntryId { get; private set; } = default!;
    public AccountId AccountId { get; private set; } = default!;
    public Money Debit { get; private set; } = default!;
    public Money Credit { get; private set; } = default!;
    public int LineNumber { get; private set; }
    public CostCenterId? CostCenterId { get; set; }
    public CostCenter? CostCenter { get; set; }

    public static JournalEntryLine Create(
        JournalEntryId journalEntryId,
        AccountId accountId,
        Money debit,
        Money credit,
        CostCenterId costCenterId,
        int lineNumber)
    {
        ArgumentNullException.ThrowIfNull(journalEntryId);
        ArgumentNullException.ThrowIfNull(accountId);
        ArgumentNullException.ThrowIfNull(debit);
        ArgumentNullException.ThrowIfNull(credit);

        return new JournalEntryLine(journalEntryId, accountId, debit, credit, costCenterId, lineNumber);
    }

    public void Update(Money debit, Money credit, CostCenterId costCenterId, int lineNumber)
    {
        ArgumentNullException.ThrowIfNull(debit);
        ArgumentNullException.ThrowIfNull(credit);

        Debit = debit;
        Credit = credit;
        CostCenterId = costCenterId;
        LineNumber = lineNumber;
    }
}