﻿namespace Accounting.Domain.Models;

/// <summary>
///     Entry header
/// </summary>
public class JournalEntry : Aggregate<JournalEntryId>
{
    private const int zero = 0;

    private readonly List<DocumentReference> _documentReferences = new();

    private readonly List<JournalEntryLine> _journalEntryLines = new();
    public IReadOnlyCollection<DocumentReference> DocumentReferences => _documentReferences.AsReadOnly();
    public IReadOnlyCollection<JournalEntryLine> JournalEntryLines => _journalEntryLines.AsReadOnly();

    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public PeriodId? PeriodId { get; private set; }

    public static JournalEntry Create(JournalEntryId id, DateTime date, string? description, PeriodId? periodId)
    {
        var journalEntry = new JournalEntry
        {
            Id = id,
            Date = date,
            Description = description,
            PeriodId = periodId
        };

        journalEntry.AddDomainEvent(new JournalEntryCreatedEvent(journalEntry));

        return journalEntry;
    }

    public void Update(string? description)
    {
        Description = description;

        AddDomainEvent(new JournalEntryUpdatedEcent(this));
    }

    public void AddLine(AccountId accountId, decimal debit, decimal credit, int lineNumber = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debit);
        ArgumentOutOfRangeException.ThrowIfNegative(credit);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        var journalEntryLine = new JournalEntryLine(Id, accountId, debit, credit, lineNumber);
        _journalEntryLines.Add(journalEntryLine);
    }

    public void AddDocumentReference(string sourceType, SourceId sourceId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        ArgumentNullException.ThrowIfNull(sourceId);

        var documentReference = new DocumentReference(Id, sourceType, sourceId);
        _documentReferences.Add(documentReference);
    }

    public void RemoveLine(JournalEntryLineId lineId)
    {
        var lineToRemove = _journalEntryLines.SingleOrDefault(l => l.Id == lineId);
        if (lineToRemove != null) _journalEntryLines.Remove(lineToRemove);
    }

    public void RemoveDocumentReference(DocumentReferenceId documentId)
    {
        var documentReference = _documentReferences.SingleOrDefault(d => d.Id == documentId);
        if (documentReference != null) _documentReferences.Remove(documentReference);
    }
}