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

    public static Period Create(PeriodId id, int year, int month)
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
            StartDate = GetFirstDayOfTheMonth(),
            EndDate = GetLastDayOfMonth(),
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

    private static DateTime GetFirstDayOfTheMonth()
    {
        var today = DateTime.Today;
        return new DateTime(today.Year, today.Month, 1);
    }

    private static DateTime GetLastDayOfMonth()
    {
        var today = DateTime.Today;
        var lastDay = DateTime.DaysInMonth(today.Year, today.Month);
        return new DateTime(today.Year, today.Month, lastDay);
    }
}