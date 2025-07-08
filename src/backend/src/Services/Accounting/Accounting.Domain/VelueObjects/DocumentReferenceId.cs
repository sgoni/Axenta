namespace Accounting.Domain.VelueObjects;

public record DocumentReferenceId
{
    private DocumentReferenceId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DocumentReferenceId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("DocumentReferenceId cannot be empty");

        return new DocumentReferenceId(value);
    }
}