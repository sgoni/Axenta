namespace Accounting.Domain.VelueObjects;

// PerformedBy
public record PerformedBy : StringValueObject
{
    public PerformedBy(string value) : base(value, 100)
    {
    }

    public static PerformedBy Of(string value) => new(value);
}