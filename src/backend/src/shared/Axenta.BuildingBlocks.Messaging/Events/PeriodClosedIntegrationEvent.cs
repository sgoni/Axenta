namespace Axenta.BuildingBlocks.Messaging.Events;

public record PeriodClosedIntegrationEvent : IntegrationEvent
{
    public PeriodClosedIntegrationEvent(Guid periodId, Guid companyId, int year, int month, DateTime closedAt,
        string? closedBy = null)
    {
        PeriodId = periodId;
        CompanyId = companyId;
        Year = year;
        Month = month;
        ClosedBy = "System";
        ClosedAt = closedAt;
    }

    public Guid PeriodId { get; set; }
    public Guid CompanyId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public string ClosedBy { get; set; }
    public DateTime ClosedAt { get; set; }
}