namespace Accounting.Domain.VelueObjects;

public record JournalEntryLineId
{
    private JournalEntryLineId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static JournalEntryLineId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("JournalEntryId cannot be empty");

        return new JournalEntryLineId(value);
    }
}