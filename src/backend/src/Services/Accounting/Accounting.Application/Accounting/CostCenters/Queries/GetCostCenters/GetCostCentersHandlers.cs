namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCenters;

public class GetCostCentersHandlers(IApplicationDbContext dbContext)
    : IQueryHandler<GetCostCentersQuery, GetCostCentersResult>
{
    public async Task<GetCostCentersResult> Handle(GetCostCentersQuery query, CancellationToken cancellationToken)
    {
        // get cost centers with pagination
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.CostCenters.LongCountAsync(cancellationToken);

        var costCenters = await dbContext.CostCenters
            .AsNoTracking()
            .Where(cc => cc.CompanyId == CompanyId.Of(query.CompanyId))
            .OrderBy(cc => cc.Code)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetCostCentersResult(
            new PaginatedResult<CostCenterDto>(
                pageIndex,
                pageSize,
                totalCount,
                costCenters.ToCostCenterDtoList()));
    }
}