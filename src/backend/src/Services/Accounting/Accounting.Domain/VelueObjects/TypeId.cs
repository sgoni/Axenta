namespace Accounting.Domain.VelueObjects;

public class TypeId
{
    private TypeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static TypeId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("TypeId cannot be empty");

        return new TypeId(value);
    }
}