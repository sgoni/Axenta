namespace Accounting.Domain.Models;

/// <summary>
///     Relationship with external modules (payments, sales)
/// </summary>
public class DocumentReference : Entity<DocumentReferenceId>
{
    internal DocumentReference(JournalEntryId journalEntryId, string sourceType, SourceId sourceId,
        DocumentReferenceNumber referenceNumber, string description)
    {
        Id = DocumentReferenceId.Of(Guid.NewGuid());
        JournalEntryId = journalEntryId;
        SourceType = sourceType;
        SourceId = sourceId;
        ReferenceNumber = referenceNumber;
        Description = description;
    }

    public JournalEntryId JournalEntryId { get; private set; } = default!;
    public string SourceType { get; private set; } = default!;
    public SourceId SourceId { get; private set; } = default!;
    public DocumentReferenceNumber ReferenceNumber { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    public static DocumentReference Create(JournalEntryId journalEntryId, string sourceType, SourceId sourceId,
        DocumentReferenceNumber referenceNumber, string description)
    {
        ArgumentNullException.ThrowIfNull(journalEntryId);
        ArgumentNullException.ThrowIfNull(sourceId);
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);

        var id = DocumentReferenceId.Of(Guid.NewGuid());
        return new DocumentReference(journalEntryId, sourceType, sourceId, referenceNumber, description);
    }
}