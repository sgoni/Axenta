namespace Accounting.Application.Extensions;

public static class PeriodExtensions
{
    public static IEnumerable<PeriodDto> ToPeriodDtoList(this IEnumerable<Period> periods)
    {
        return periods.Select(period =>
            new PeriodDto(
                period.Id.Value,
                period.CompanyId.Value,
                period.Year,
                period.Month,
                period.StartDate,
                period.EndDate,
                period.IsClosed
            ));
    }

    public static PeriodDto DtoFromPeriod(this Period period)
    {
        return new PeriodDto(
            period.Id.Value,
            period.CompanyId.Value,
            period.Year,
            period.Month,
            period.StartDate,
            period.EndDate,
            period.IsClosed);
    }
}