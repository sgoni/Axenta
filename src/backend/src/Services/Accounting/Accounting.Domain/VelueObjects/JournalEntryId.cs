namespace Accounting.Domain.VelueObjects;

public record JournalEntryId
{
    private JournalEntryId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static JournalEntryId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("JournalEntryId cannot be empty");

        return new JournalEntryId(value);
    }
}