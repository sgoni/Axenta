namespace Axenta.BuildingBlocks.Messaging.Events;

// Se dispara al revertir un asiento
public record JournalEntryReversedIntegrationEvent(
    Guid JournalEntryId,
    DateTime ReversedAt,
    string ReversedBy,
    string? CorrelationId = null
);