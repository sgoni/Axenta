namespace Accounting.Domain.VelueObjects;

public record AccountingPolicyTemplateId
{
    private AccountingPolicyTemplateId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AccountingPolicyTemplateId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("AccountingPolicyTemplateId cannot be empty");

        return new AccountingPolicyTemplateId(value);
    }
}