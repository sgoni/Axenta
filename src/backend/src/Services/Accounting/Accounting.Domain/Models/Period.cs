namespace Accounting.Domain.Models;

/// <summary>
///     Control of open/closed periods
/// </summary>
public class Period : Aggregate<PeriodId>
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public CompanyId CompanyId { get; private set; }
    public bool IsClosed { get; private set; }

    public static Period Create(CompanyId companyId, PeriodId id, int year, int month)
    {
        ArgumentOutOfRangeException.ThrowIfZero(year);
        ArgumentOutOfRangeException.ThrowIfZero(month);
        ArgumentOutOfRangeException.ThrowIfLessThan(month, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(month, 12);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(companyId.ToString());

        var period = new Period
        {
            Id = id,
            Year = year,
            Month = month,
            StartDate = GetFirstDayOfTheMonth(),
            EndDate = GetLastDayOfMonth(),
            CompanyId = companyId,
            IsClosed = false
        };

        return period;
    }

    public void Close()
    {
        IsClosed = true;
        // Trigger event
        AddDomainEvent(new PeriodClosedDomainEvent(Id.Value, Year, Month));
    }

    public void Reopen(IEnumerable<JournalEntry> closingEntries)
    {
        if (!IsClosed)
            throw new DomainException("The period is now open");

        // Reverse all closing entries
        foreach (var entry in closingEntries.Where(e => e.JournalEntryType == JournalEntryType.Normal.Name))
        {
            var reversal = entry.Reverse();
            // Here you must persist the reversal in the application handler
            AddDomainEvent(new JournalEntryReversedDomainEvent(reversal.Id.Value));
        }

        IsClosed = false;
        AddDomainEvent(new PeriodReopenedDomainEvent(Id.Value, Year, Month));
    }

    private static DateTime GetFirstDayOfTheMonth()
    {
        var today = DateTime.Today;
        return DateTime.SpecifyKind(new DateTime(today.Year, today.Month, 1), DateTimeKind.Utc);
    }

    private static DateTime GetLastDayOfMonth()
    {
        var today = DateTime.Today;
        var lastDay = DateTime.DaysInMonth(today.Year, today.Month);
        return DateTime.SpecifyKind(new DateTime(today.Year, today.Month, lastDay), DateTimeKind.Utc);
    }
}