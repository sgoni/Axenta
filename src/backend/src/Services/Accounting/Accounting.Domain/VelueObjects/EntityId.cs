namespace Accounting.Domain.VelueObjects;

public record EntityId : GuidValueObject
{
    public EntityId(Guid value) : base(value)
    {
    }

    public static EntityId Of(Guid value)
    {
        return new EntityId(value);
    }
}