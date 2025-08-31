namespace Accounting.Domain.VelueObjects;

public record JournalEntryId : GuidValueObject
{
    public JournalEntryId(Guid value) : base(value)
    {
    }

    public static JournalEntryId Of(Guid value) => new(value);
}