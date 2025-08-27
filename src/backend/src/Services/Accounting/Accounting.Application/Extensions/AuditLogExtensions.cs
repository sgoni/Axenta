namespace Accounting.Application.Extensions;

public static class AuditLogExtensions
{
    public static IEnumerable<AuditLogDto> ToAuditLogsDtoList(this IEnumerable<AuditLog> auditLogs)
    {
        return auditLogs.Select(auditLog =>
            new AuditLogDto(
                auditLog.Id.Value,
                auditLog.Entity,
                auditLog.Action,
                auditLog.PerformedBy.Value,
                auditLog.PerformedAt,
                auditLog.Details
            ));
    }

    public static AuditLogDto ToAuditLogDto(this AuditLog auditLogDto)
    {
        return DtoFromAuditLog(auditLogDto);
    }

    public static AuditLogDto DtoFromAuditLog(AuditLog auditLog)
    {
        return new AuditLogDto(
            auditLog.Id.Value,
            auditLog.Entity,
            auditLog.Action,
            auditLog.PerformedBy.Value,
            auditLog.PerformedAt,
            auditLog.Details
        );
    }
}