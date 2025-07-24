namespace Accounting.Application.Accounting.Periods.Queries.GetPeriods;

public record GetPeriodsQuery(PaginationRequest PaginationRequest) : IQuery<GetPeriodsResult>;

public record GetPeriodsResult(PaginatedResult<PeriodDto> Periods);