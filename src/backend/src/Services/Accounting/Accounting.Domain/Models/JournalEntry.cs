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
    public string JournalEntryType { get; private set; } = Enums.JournalEntryType.Normal.Name;
    public JournalEntryId? ReversalJournalEntryId { get; private set; }

    public static JournalEntry Create(JournalEntryId id, DateTime date, string? description, PeriodId? periodId,
        CompanyId companyId, string? currencyCode, decimal? exchangeRate, DateOnly? exchangeRateDate)
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
            JournalEntryType = Enums.JournalEntryType.Normal.Name
        };

        journalEntry.AddDomainEvent(new JournalEntryCreatedEvent(journalEntry));

        return journalEntry;
    }

    public void Update(string? description, DateTime date, string? currencyCode, decimal? exchangeRate,
        DateOnly? exchangeRateDate, string journalEntryType)
    {
        var before = new JournalEntry
        {
            Id = Id,
            PeriodId = PeriodId,
            CompanyId = CompanyId,
            Description = Description,
            Date = Date,
            CurrencyCode = CurrencyCode,
            ExchangeRate = ExchangeRate,
            ExchangeRateDate = ExchangeRateDate,
            JournalEntryType = JournalEntryType,
            ReversalJournalEntryId = ReversalJournalEntryId
        };

        Description = description;
        Date = date;
        CurrencyCode = currencyCode;
        ExchangeRate = exchangeRate;
        ExchangeRateDate = exchangeRateDate;
        JournalEntryType = journalEntryType;

        AddDomainEvent(new JournalEntryUpdatedEvent(before, this));
    }

    public JournalEntry Reverse()
    {
        if (JournalEntryType.Equals(Enums.JournalEntryType.Reversal.Name))
            throw new DomainException("The seat has already been reversed.");

        // Create a reverse entry
        var reversal = new JournalEntry
        {
            Id = JournalEntryId.Of(Guid.NewGuid()),
            Date = DateTime.UtcNow,
            Description = $"Reversal of entry {Id.Value}",
            PeriodId = PeriodId,
            CompanyId = CompanyId,
            CurrencyCode = CurrencyCode,
            ExchangeRate = ExchangeRate,
            ExchangeRateDate = ExchangeRateDate,
            ReversalJournalEntryId = Id,
            JournalEntryType = Enums.JournalEntryType.Normal.Name
        };

        // Mark original as reversed
        foreach (var line in _journalEntryLines)
            reversal.AddLine(
                line.AccountId,
                Money.Of(line.Credit, CurrencyCode),
                Money.Of(line.Debit, CurrencyCode),
                line.LineNumber
            );

        // Mark original as reversed
        JournalEntryType = Enums.JournalEntryType.Reversal.Name;
        ReversalJournalEntryId = reversal.Id;

        // Fire domain event
        AddDomainEvent(new JournalEntryReversedDomainEvent(Id.Value));

        return reversal;
    }

    public void AddLine(AccountId accountId, Money debit, Money credit, int lineNumber = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debit.Amount);
        ;
        ArgumentOutOfRangeException.ThrowIfNegative(credit.Amount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        var journalEntryLine = new JournalEntryLine(Id, accountId, debit, credit, lineNumber);
        _journalEntryLines.Add(journalEntryLine);
    }

    public void UpdateLine(JournalEntryLineId IdLine, AccountId accountId, Money debit, Money credit,
        int lineNumber = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debit.Amount);
        ArgumentOutOfRangeException.ThrowIfNegative(credit.Amount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        var line = _journalEntryLines.SingleOrDefault(x => x.Id == IdLine);
        RemoveLine(line.Id);

        var journalEntryLine = new JournalEntryLine(Id, accountId, debit, credit, lineNumber);
        _journalEntryLines.Add(journalEntryLine);
    }

    public void AddDocumentReference(string sourceType, SourceId sourceId, DocumentReferenceNumber referenceNumber,
        string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        ArgumentNullException.ThrowIfNull(sourceId);

        var documentReference = DocumentReference.Create(Id, sourceType, sourceId, referenceNumber, description);
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