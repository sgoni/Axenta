namespace Accounting.Domain.VelueObjects;

public record CostCenterId : GuidValueObject
{
    public CostCenterId(Guid value) : base(value)
    {
    }

    public static CostCenterId Of(Guid value)
    {
        return new CostCenterId(value);
    }

    public static CostCenterId? FromNullable(Guid? parentAccountId)
    {
        if (!parentAccountId.HasValue || parentAccountId == Guid.Empty) return null;
        return new CostCenterId(parentAccountId.Value);
    }
}