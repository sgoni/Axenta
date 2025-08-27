namespace Accounting.Domain.Models;

/// <summary>
///     Change log for compliance
/// </summary>
public class AuditLog : Entity<AuditLogId>
{
    public string Entity { get; private set; } = default!;
    public EntityId EntityId { get; private set; } = default!;
    public string Action { get; private set; } = default!; // Insert, Update, Delete
    public PerformedBy? PerformedBy { get; private set; }
    public DateTime PerformedAt { get; private set; }
    public string? Details { get; private set; }

    public static AuditLog Create(AuditLogId id, string entity, EntityId entityId, string action,
        PerformedBy? performedBy, string? details = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entity);
        ArgumentNullException.ThrowIfNull(entityId);
        ArgumentException.ThrowIfNullOrWhiteSpace(action);
        ArgumentNullException.ThrowIfNull(performedBy);

        var auditLog = new AuditLog
        {
            Id = id,
            Entity = entity,
            EntityId = entityId,
            Action = action,
            PerformedBy = performedBy,
            PerformedAt = DateTime.UtcNow,
            Details = details
        };

        return auditLog;
    }
}