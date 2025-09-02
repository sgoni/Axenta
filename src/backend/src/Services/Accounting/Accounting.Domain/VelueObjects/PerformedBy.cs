namespace Accounting.Domain.VelueObjects;

// PerformedBy
public record PerformedBy : GuidValueObject
{
    public PerformedBy(Guid value) : base(value)
    {
    }

    public static PerformedBy Of(Guid value)
    {
        return new PerformedBy(value);
    }
}