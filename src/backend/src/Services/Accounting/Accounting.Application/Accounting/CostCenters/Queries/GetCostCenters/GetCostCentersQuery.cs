namespace Accounting.Application.Accounting.CostCenters.Queries.GetCostCenters;

public record GetCostCentersQuery(PaginationRequest PaginationRequest, Guid CompanyId) : IQuery<GetCostCentersResult>;

public record GetCostCentersResult(PaginatedResult<CostCenterDto> CostCenters);