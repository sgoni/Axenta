namespace Accounting.Application.Accounting.Periods.Queries.GetPeriodById;

public record GetPeriodByIdQuery(Guid PeriodId) : IQuery<GetPeriodByIdQueryResult>;

public record GetPeriodByIdQueryResult(PeriodDto PeriodDetail);