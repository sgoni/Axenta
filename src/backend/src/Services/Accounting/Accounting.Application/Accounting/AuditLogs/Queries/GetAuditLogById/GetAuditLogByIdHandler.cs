namespace Accounting.Application.Accounting.AuditLogs.Queries.GetAuditLogById;

public class GetAuditLogByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAuditLogByIdQuery, GetAuditLogByIdQueryResult>
{
    public async Task<GetAuditLogByIdQueryResult> Handle(GetAuditLogByIdQuery query,
        CancellationToken cancellationToken)
    {
        // get auditlog by Id using dbContext
        // return result

        var audilogId = AuditLogId.Of(query.AuditLogId);
        var auditLog = await dbContext.AuditLogs.FindAsync(audilogId, cancellationToken);

        if (auditLog == null) throw new AuditlogNotFoundException(query.AuditLogId);

        return new GetAuditLogByIdQueryResult(auditLog.DtoFromAuditLog());
    }
}