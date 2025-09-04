namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCentersTree;

public class GetCostCentersTreeHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCostCentersTreeQuery, GetCostCentersTreeResult>
{
    public async Task<GetCostCentersTreeResult> Handle(GetCostCentersTreeQuery query,
        CancellationToken cancellationToken)
    {
        var costCenters = await dbContext.CostCenters
            .Include(cc => cc.Children)
            .AsNoTracking()
            .Where(cc => cc.CompanyId == CompanyId.Of(query.CompanyId))
            .OrderBy(cc => cc.Code)
            .ToListAsync(cancellationToken);

        var flatCostCenters = costCenters.ToCostCenterTreeDtoList();

        var tree = CostCenterExtensions.BuildTree(flatCostCenters);

        return new GetCostCentersTreeResult(tree);
    }
}