namespace Accounting.Application.Accounting.AuditLogs.Queries.GetAuditLogs;

public record GetAuditLogsQuery(PaginationRequest PaginationRequest) : IQuery<GetAuditLogsResult>;

public record GetAuditLogsResult(PaginatedResult<AuditLogDto> AuditLogs);