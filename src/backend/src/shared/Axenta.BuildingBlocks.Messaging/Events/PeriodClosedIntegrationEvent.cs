namespace Axenta.BuildingBlocks.Messaging.Events;

public record PeriodClosedIntegrationEvent : IntegrationEvent
{
    public PeriodClosedIntegrationEvent(Guid periodId, Guid companyId, DateTime closedAt)
    {
        PeriodId = periodId;
        CompanyId = companyId;
        ClosedAt = closedAt;
    }

    public Guid PeriodId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime ClosedAt { get; set; }
}