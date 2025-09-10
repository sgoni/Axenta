namespace Axenta.BuildingBlocks.Messaging.Events;

// Se dispara al revertir un asiento
public record JournalEntryReversedIntegrationEvent : IntegrationEvent
{
    public JournalEntryReversedIntegrationEvent(Guid journalEntryId, DateTime reversedAt, string reversedBy,
        string? correlationId = null)
    {
        JournalEntryId = journalEntryId;
        ReversedAt = reversedAt;
        ReversedBy = reversedBy;
        CorrelationId = correlationId;
    }

    public Guid JournalEntryId { get; set; }
    public DateTime ReversedAt { get; set; }
    public string? ReversedBy { get; set; }
    public string? CorrelationId { get; set; }
}