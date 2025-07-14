namespace Accounting.Domain.VelueObjects;

public record SourceId
{
    private SourceId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SourceId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("SourceId cannot be empty");

        return new SourceId(value);
    }
}