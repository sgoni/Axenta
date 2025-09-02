using Accounting.Application.Accounting.CostCenters.Queries.GetCostCentersTree;

namespace Accounting.API.Endpoints.CostCenter;

//public record GetAccountsTreeRequest(PaginationRequest PaginationRequest);
public record GetCostCentersTreeResponse(IEnumerable<CostCenterTreeDto> CostCenterTree);

public class GetCostCenterTree : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("cost-centers/{companyId}/tree", async (Guid companyId, ISender sender) =>
            {
                var result = await sender.Send(new GetCostCentersTreeQuery(companyId));

                var response = result.Adapt<GetCostCentersTreeResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCostCentersTree")
            .Produces<GetCostCentersTreeResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Return hierarchical tree of cost centers by company")
            .WithDescription("Return hierarchical tree of cost centers by company");
    }
}