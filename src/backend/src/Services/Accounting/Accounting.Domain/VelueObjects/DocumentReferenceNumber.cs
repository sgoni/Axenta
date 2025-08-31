namespace Accounting.Domain.VelueObjects;

public record DocumentReferenceNumber
{
    private DocumentReferenceNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static DocumentReferenceNumber Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > 150)
            throw new DomainException("DocumentReferenceNumber cannot exceed 150 characters.");

        // Example: validate that it is alphanumeric with scripts or bars
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Za-z0-9\-\/]+$"))
            throw new DomainException("DocumentReferenceNumber must be alphanumeric, allowing '-' and '/'.");

        return new DocumentReferenceNumber(value);
    }

    public override string ToString() => Value;
}