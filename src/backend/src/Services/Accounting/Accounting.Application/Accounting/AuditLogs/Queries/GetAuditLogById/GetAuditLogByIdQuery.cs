namespace Accounting.Application.Accounting.AuditLogs.Queries.GetAuditLogById;

public record GetAuditLogByIdQuery(Guid AuditLogId) : IQuery<GetAuditLogByIdQueryResult>;

public record GetAuditLogByIdQueryResult(AuditLogDto AuditLogDetail);