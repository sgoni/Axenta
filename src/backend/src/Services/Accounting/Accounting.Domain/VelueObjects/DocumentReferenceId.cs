namespace Accounting.Domain.VelueObjects;

public record DocumentReferenceId : GuidValueObject
{
    public DocumentReferenceId(Guid value) : base(value)
    {
    }

    public static DocumentReferenceId Of(Guid value)
    {
        return new DocumentReferenceId(value);
    }
}