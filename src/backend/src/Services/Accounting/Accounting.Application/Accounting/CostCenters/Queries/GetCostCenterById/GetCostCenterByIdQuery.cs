namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCenterById;

public record GetCostCenterByIdQuery(Guid CostCenterId) : IQuery<GetCostCenterByIdResult>;

public record GetCostCenterByIdResult(CostCenterDto CostCenterDetail);