using Accounting.Application.Accounting.CostCenters.Queries.GetCostCenterById;

namespace Accounting.API.Endpoints.CostCenter;

//- Accepts a cost center ID.
//- Uses a GetCostcenterByIdQuery to fetch orders.
//- Returns the list of orders for that customer.m

//public record GetCostCenterByIdRequest(Guid costCenterId);

public record GetCostCenterByIdResponse(CostCenterDto CostCenterDetail);

public class GetCostCenterById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cost-centers/{costCenterId}/{companyId}",
                async (Guid costCenterId, Guid companyId, ISender sender) =>
                {
                    var result = await sender.Send(new GetCostCenterByIdQuery(costCenterId, companyId));

                    var response = result.Adapt<GetCostCenterByIdResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetCostCentertById")
            .Produces<GetCostCenterByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Detail of a cost center")
            .WithDescription("Detail of a cost center.");
    }
}