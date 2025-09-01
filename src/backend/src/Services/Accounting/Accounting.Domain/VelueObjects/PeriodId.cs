namespace Accounting.Domain.VelueObjects;

public record PeriodId : GuidValueObject
{
    public PeriodId(Guid value) : base(value)
    {
    }

    public static PeriodId Of(Guid value)
    {
        return new PeriodId(value);
    }
}