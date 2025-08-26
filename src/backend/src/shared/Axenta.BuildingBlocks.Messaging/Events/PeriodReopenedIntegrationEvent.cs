namespace Axenta.BuildingBlocks.Messaging.Events;

public record PeriodReopenedIntegrationEvent : IntegrationEvent
{
    public Guid PeriodId { get; set; } = default!;
    public Guid CompanyId { get; set; } = default!;
    public DateTime ReopenedAt { get; set; } = default!;

    public PeriodReopenedIntegrationEvent(Guid periodId, Guid companyId, DateTime reopenedAt)
    {
        PeriodId = periodId;
        CompanyId = companyId;
        ReopenedAt = reopenedAt;
    }
}