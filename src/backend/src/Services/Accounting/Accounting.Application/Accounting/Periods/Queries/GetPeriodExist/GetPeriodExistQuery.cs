namespace Accounting.Application.Accounting.Periods.Queries.GetPeriodExist;

public record GetPeriodExistQuery(int year, int month) : IQuery<GetPeriodExistResult>;

public record GetPeriodExistResult(bool Exists);