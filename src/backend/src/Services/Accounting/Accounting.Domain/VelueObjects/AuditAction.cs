namespace Accounting.Domain.VelueObjects;

// Action (AuditLog)
public record AuditAction : StringValueObject
{
    public AuditAction(string value) : base(value, 50) { }

    public static AuditAction Of(string value) => new(value);
}