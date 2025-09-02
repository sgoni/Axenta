using MapsterMapper;

namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCenterById;

public class GetCostCenterByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCostCenterByIdQuery, GetCostCenterByIdResult>
{
    public async Task<GetCostCenterByIdResult> Handle(GetCostCenterByIdQuery query, CancellationToken cancellationToken)
    {
        // get cost center by Id using dbContext
        // return result
        var costCenterId = CostCenterId.Of(query.CostCenterId);
        var costCenter = await dbContext.CostCenters.FindAsync([costCenterId], cancellationToken);

        if (costCenter is null) throw EntityNotFoundException.For<CostCenter>(query.CostCenterId);

        return new GetCostCenterByIdResult(costCenter.ToCostCenterDto());
    }
}