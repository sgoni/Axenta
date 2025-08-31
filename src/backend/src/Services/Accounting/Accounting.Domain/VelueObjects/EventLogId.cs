namespace Accounting.Domain.VelueObjects;

public record EventLogId : GuidValueObject
{
    public EventLogId(Guid value) : base(value)
    {
    }

    public static EventLogId Of(Guid value) => new(value);
}