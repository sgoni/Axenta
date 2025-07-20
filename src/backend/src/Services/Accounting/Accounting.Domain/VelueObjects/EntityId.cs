namespace Accounting.Domain.VelueObjects;

public record EntityId
{
    private EntityId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EntityId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("EntityId cannot be empty");

        return new EntityId(value);
    }
}