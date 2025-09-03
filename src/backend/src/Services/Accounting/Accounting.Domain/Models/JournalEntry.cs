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
                line.Credit,
                line.Debit,
                line.CostCenterId,
                line.LineNumber
            );

        // Mark original as reversed
        JournalEntryType = Enums.JournalEntryType.Reversal.Name;
        ReversalJournalEntryId = reversal.Id;

        // Fire domain event
        AddDomainEvent(new JournalEntryReversedDomainEvent(Id.Value));

        return reversal;
    }

    public void AddLine(AccountId accountId, Money debit, Money credit, CostCenterId? costCenterId, int lineNumber = 1)
    {
        ArgumentNullException.ThrowIfNull(accountId);
        ArgumentNullException.ThrowIfNull(debit);
        ArgumentNullException.ThrowIfNull(credit);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        // If the seat declares currency at the heading level, all lines must match
        if (!string.IsNullOrWhiteSpace(CurrencyCode))
            if (!CurrencyCode!.Equals(debit.CurrencyCode, StringComparison.OrdinalIgnoreCase) ||
                !CurrencyCode!.Equals(credit.CurrencyCode, StringComparison.OrdinalIgnoreCase))
                throw new DomainException("Line currency must match JournalEntry currency.");

        // Optional/Recommended: If the header does not define currency, force the company's base currency 
        // Var Basecurrency = Company.Basecurrency; // If you have it in Company 
        // if (debit.currencycode!
        var journalEntryLine = JournalEntryLine.Create(Id, accountId, debit, credit, costCenterId, lineNumber);
        _journalEntryLines.Add(journalEntryLine);
    }

    public void UpdateLine(JournalEntryLineId IdLine, AccountId accountId, Money debit, Money credit,
        CostCenterId costCenterId, int lineNumber = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(debit.Amount);
        ArgumentOutOfRangeException.ThrowIfNegative(credit.Amount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lineNumber);

        var line = _journalEntryLines.SingleOrDefault(x => x.Id == IdLine);
        line.Update(line.Debit, line.Credit, costCenterId, line.LineNumber);
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
        if (!_journalEntryLines.Any())
            throw new DomainException("Journal entry must have at least one line.");

        var totalDebit = _journalEntryLines.Select(l => l.Debit)
            .Aggregate(Money.Of(0, _journalEntryLines.First().Debit.CurrencyCode), (acc, m) => acc.Add(m));

        var totalCredit = _journalEntryLines.Select(l => l.Credit)
            .Aggregate(Money.Of(0, _journalEntryLines.First().Credit.CurrencyCode), (acc, m) => acc.Add(m));

        if (totalDebit.Amount != totalCredit.Amount)
            throw new DomainException(
                $"Unbalanced entry. Debit {totalDebit.Amount} {totalDebit.CurrencyCode} != Credit {totalCredit.Amount} {totalCredit.CurrencyCode}");
    }
}