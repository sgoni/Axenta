namespace Accounting.Application.Accounting.Periods.Queries.GetPeriods;

public class GetPeriodsHandler(IApplicationDbContext dbContext) : IQueryHandler<GetPeriodsQuery, GetPeriodsResult>
{
    public async Task<GetPeriodsResult> Handle(GetPeriodsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Accounts.LongCountAsync(cancellationToken);

        var periods = await dbContext.Periods
            .AsNoTracking()
            .OrderBy(p => p.StartDate)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetPeriodsResult(
            new PaginatedResult<PeriodDto>(
                pageIndex,
                pageSize,
                totalCount,
                periods.ToPeriodDtoList()));
    }
}