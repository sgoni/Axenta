namespace Accounting.Domain.Models;

/// <summary>
///     Control of open/closed periods
/// </summary>
public class Period : Entity<PeriodId>
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsClosed { get; private set; }

    public static Period Create(PeriodId id, int year, int month, DateTime start, DateTime end)
    {
        ArgumentOutOfRangeException.ThrowIfZero(year);
        ArgumentOutOfRangeException.ThrowIfZero(month);
        ArgumentOutOfRangeException.ThrowIfLessThan(month, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(month, 12);

        var period = new Period
        {
            Id = id,
            Year = year,
            Month = month,
            StartDate = start,
            EndDate = end,
            IsClosed = false
        };

        return period;
    }

    public void Close()
    {
        IsClosed = true;
    }

    public void Open()
    {
        IsClosed = false;
    }
}