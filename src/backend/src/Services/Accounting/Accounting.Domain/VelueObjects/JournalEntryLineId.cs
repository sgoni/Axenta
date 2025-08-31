namespace Accounting.Domain.VelueObjects;

public record JournalEntryLineId : GuidValueObject
{
    public JournalEntryLineId(Guid value) : base(value)
    {
    }

    public static JournalEntryLineId Of(Guid value) => new(value);
}