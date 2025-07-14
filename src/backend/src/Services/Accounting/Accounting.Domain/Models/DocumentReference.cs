namespace Accounting.Domain.Models;

/// <summary>
///     Relationship with external modules (payments, sales)
/// </summary>
public class DocumentReference : Entity<DocumentReferenceId>
{
    internal DocumentReference(JournalEntryId journalEntryId, string sourceType, SourceId sourceId)
    {
        Id = DocumentReferenceId.Of(Guid.NewGuid());
        JournalEntryId = journalEntryId;
        SourceType = sourceType;
        SourceId = sourceId;
    }

    public JournalEntryId JournalEntryId { get; private set; } = default!;
    public string SourceType { get; private set; } = default!;
    public SourceId SourceId { get; private set; } = default!;
}