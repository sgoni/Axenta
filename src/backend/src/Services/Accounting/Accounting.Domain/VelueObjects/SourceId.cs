namespace Accounting.Domain.VelueObjects;

public record SourceId: GuidValueObject
{
    public SourceId(Guid value) : base(value)
    {
    }

    public static SourceId Of(Guid value) => new(value);
}