namespace Accounting.Domain.VelueObjects;

public record AuditLogId
{
    private AuditLogId(Guid value) => Value = value;
    public Guid Value { get; }

    public static AuditLogId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("AuditLogId cannot be empty");

        return new AuditLogId(value);
    }
}