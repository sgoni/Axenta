namespace Axenta.BuildingBlocks.Messaging.Events;

public class PeriodClosedIntegrationEvent
{
    public PeriodClosedIntegrationEvent(Guid periodId, Guid companyId, DateTime reopenedAt)
    {
        PeriodId = periodId;
        CompanyId = companyId;
        ReopenedAt = reopenedAt;
    }

    public Guid PeriodId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime ReopenedAt { get; set; }
}