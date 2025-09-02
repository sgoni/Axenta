namespace Accounting.Domain.VelueObjects;

public record AccountingPolicyTemplateId : GuidValueObject
{
    public AccountingPolicyTemplateId(Guid value) : base(value)
    {
    }

    public static AccountingPolicyTemplateId Of(Guid value)
    {
        return new AccountingPolicyTemplateId(value);
    }
}