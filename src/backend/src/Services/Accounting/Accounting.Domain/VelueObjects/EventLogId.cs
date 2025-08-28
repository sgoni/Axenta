namespace Accounting.Domain.VelueObjects;

public record EventLogId
{
    private EventLogId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EventLogId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("EventLogId cannot be empty");

        return new EventLogId(value);
    }
}