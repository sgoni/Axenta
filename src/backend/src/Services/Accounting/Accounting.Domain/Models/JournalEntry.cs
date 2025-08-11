namespace Accounting.Domain.Models;

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
    public PeriodId? PeriodId { get; private set; }
    public CompanyId CompanyId { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public string? CurrencyCode { get; private set; } // null = moneda local
    public decimal? ExchangeRate { get; private set; }
    public DateOnly? ExchangeRateDate { get; private set; }

    public bool IsPosted { get; private set; } = true;
    public bool IsReversed { get; private set; }
    public JournalEntryId ReversalJournalEntryId { get; private set; }

    public static JournalEntry Create(JournalEntryId id, DateTime date, string? description, PeriodId? periodId,
        CompanyId companyId, string currencyCode, decimal? exchangeRate, DateOnly? exchangeRateDate)
    {
        var journalEntry = new JournalEntry
        {
            Id = id,
            Date = date,
            Description = description,
            PeriodId = periodId,
            CompanyId = companyId,
            CurrencyCode = currencyCode,
            ExchangeRate = exchangeRate,
            ExchangeRateDate = exchangeRateDate,
            IsPosted = true,
            IsReversed = false
        };

        journalEntry.AddDomainEvent(new JournalEntryCreatedEvent(journalEntry));

        return journalEntry;
    }

    public void Update(string? description, DateTime date, string currencyCode, bool isPosted = false)
    {
        Description = description;
        Date = date;
        IsPosted = isPosted;
        CurrencyCode = currencyCode;

        AddDomainEvent(new JournalEntryUpdatedEvent(this));
    }

    public JournalEntry Reverse()
    {
        if (IsReversed)
            throw new DomainException("The seat has already been reversed.");

        // Create reverse entry
        var reversal = new JournalEntry
        {
            Id = JournalEntryId.Of(Guid.NewGuid()),
            Date = DateTime.UtcNow,
            Description = $"Reversal of entry {Id.Value}",
            CurrencyCode = CurrencyCode,
            ExchangeRate = ExchangeRate,
            ExchangeRateDate = ExchangeRateDate,
            PeriodId = PeriodId,
            IsPosted = true,
            IsReversed = false,
            ReversalJournalEntryId = Id
        };

        // Mark original as reversed
        foreach (var line in _journalEntryLines)
            reversal.AddLine(
                line.AccountId,
                line.Credit,
                line.Debit,
                line.LineNumber
            );

        // Mark original as reversed
        IsReversed = true;
        ReversalJournalEntryId = reversal.Id;

        // Fire domain event
        AddDomainEvent(new JournalEntryReversedDomainEvent(Id.Value, reversal.Id.Value));

        return reversal;
    }

    public void AddLine(AccountId accountId, decimal debit, decimal credit, int lineNumber = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debit);
        ArgumentOutOfRangeException.ThrowIfNegative(credit);
        ArgumentOutOfRangeException.ThrowIfNegative(credit);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        var journalEntryLine = new JournalEntryLine(Id, accountId, debit, credit, lineNumber);
        _journalEntryLines.Add(journalEntryLine);
    }

    public void UpdateLine(JournalEntryLineId IdLine, AccountId accountId, decimal debit, decimal credit,
        int lineNumber = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debit);
        ArgumentOutOfRangeException.ThrowIfNegative(credit);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        var line = _journalEntryLines.SingleOrDefault(x => x.Id == IdLine);
        RemoveLine(line.Id);

        var journalEntryLine = new JournalEntryLine(Id, accountId, debit, credit, lineNumber);
        _journalEntryLines.Add(journalEntryLine);
    }

    public void Posted()
    {
        IsPosted = true;
    }

    public void AddDocumentReference(string sourceType, SourceId sourceId, string referenceNumber, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        ArgumentNullException.ThrowIfNull(sourceId);

        var documentReference = new DocumentReference(Id, sourceType, sourceId, referenceNumber, description);
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

    public void ValidateBalance()
    {
        var totalDebit = _journalEntryLines.Sum(l => l.Debit);
        var totalCredit = _journalEntryLines.Sum(l => l.Credit);

        if (totalDebit != totalCredit)
            throw new DomainException("The accounting seat does not square: debits ≠ credits.");
    }
}