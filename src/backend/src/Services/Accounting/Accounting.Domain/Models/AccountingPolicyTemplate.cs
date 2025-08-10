namespace Accounting.Domain.Models;

public class AccountingPolicyTemplate : Entity<AccountingPolicyTemplateId>
{
    private readonly List<AccountingPolicyLine> _lines = new();
    public IReadOnlyCollection<AccountingPolicyLine> Lines => _lines.AsReadOnly();

    public string Name { get; private set; }
    public string Trigger { get; private set; } // Ej: "MonthEnd", "InvoicePosted"

    public static AccountingPolicyTemplate Create(AccountingPolicyTemplateId id, string name, string trigger)
    {
        var accountingPolicyTemplate = new AccountingPolicyTemplate
        {
            Id = id,
            Name = name,
            Trigger = trigger
        };

        return accountingPolicyTemplate;
    }

    public void AddLine(AccountId accountId, decimal debit, decimal credit)
    {
        _lines.Add(new AccountingPolicyLine(accountId, debit, credit));
    }
}

public record AccountingPolicyLine(AccountId AccountId, decimal Debit, decimal Credit);