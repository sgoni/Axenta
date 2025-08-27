namespace Accounting.Application.Accounting.Periods.EventHandlers.Domain;

public class PeriodClosedEventHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    ILogger<PeriodClosedEventHandler> logger) : INotificationHandler<PeriodClosedDomainEvent>
{
    public async Task Handle(PeriodClosedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        var periodDomainEvent = domainEvent.PeriodId;

        var auditLog = CreateNewAuditLog(periodDomainEvent);
        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private AuditLog CreateNewAuditLog(Guid periodId)
    {
        var auditLog = AuditLog.Create(
            AuditLogId.Of(Guid.NewGuid()),
            "Period",
            EntityId.Of(PeriodId.Of(periodId).Value),
            "Create",
            PerformedBy.Of(new Guid("d1521f2b-7690-467d-9fe3-4d2ee00f6950")),
            $"Period closure {periodId}"
        );

        return auditLog;
    }
}