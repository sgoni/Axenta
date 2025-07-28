namespace Accounting.Application.Accounting.Periods.Queries.GetPeriodExist;

public class GetPeriodExistHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPeriodExistQuery, GetPeriodExistResult>
{
    public async Task<GetPeriodExistResult> Handle(GetPeriodExistQuery query, CancellationToken cancellationToken)
    {
        bool exists =
            await dbContext.Periods.AnyAsync(p => p.Year == query.year && p.Month == query.month, cancellationToken);

        return new GetPeriodExistResult(exists);
    }
}