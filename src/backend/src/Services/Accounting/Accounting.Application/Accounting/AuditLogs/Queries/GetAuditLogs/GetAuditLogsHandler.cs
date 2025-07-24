namespace Accounting.Application.Accounting.AuditLogs.Queries.GetAuditLogs;

public class GetAuditLogsHandler(IApplicationDbContext dbContext) : IQueryHandler<GetAuditLogsQuery, GetAuditLogsResult>
{
    public async Task<GetAuditLogsResult> Handle(GetAuditLogsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Accounts.LongCountAsync(cancellationToken);

        var auditLogs = await dbContext.AuditLogs
            .AsNoTracking()
            .OrderBy(al => al.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetAuditLogsResult(
            new PaginatedResult<AuditLogDto>(
                pageIndex,
                pageSize,
                totalCount,
                auditLogs.ToAuditLogsDtoList()));
    }
}