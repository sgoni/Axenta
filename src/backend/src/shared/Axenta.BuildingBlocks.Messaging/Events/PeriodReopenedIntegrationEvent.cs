namespace Axenta.BuildingBlocks.Messaging.Events;

public record PeriodReopenedIntegrationEvent : IntegrationEvent
{
    public PeriodReopenedIntegrationEvent(Guid periodId, Guid companyId, int year, int month, DateTime closedAt,
        string? closedBy = null)
    {
        PeriodId = periodId;
        CompanyId = companyId;
        Year = year;
        Month = month;
        ReopenedBy = "System";
        ReopenedAt = closedAt;
    }

    public Guid PeriodId { get; set; }
    public Guid CompanyId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public string ReopenedBy { get; set; }
    public DateTime ReopenedAt { get; set; }
}