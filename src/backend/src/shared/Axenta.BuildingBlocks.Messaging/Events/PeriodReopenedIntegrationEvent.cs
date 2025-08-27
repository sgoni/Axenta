namespace Axenta.BuildingBlocks.Messaging.Events;

public record PeriodReopenedIntegrationEvent : IntegrationEvent
{
    public PeriodReopenedIntegrationEvent(Guid periodId, Guid companyId, DateTime reopenedAt)
    {
        PeriodId = periodId;
        CompanyId = companyId;
        ReopenedAt = reopenedAt;
    }

    public Guid PeriodId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime ReopenedAt { get; set; }
}