namespace Accounting.API.Endpoints.CostCenter;

//- Accepts pagination parameters.
//- Constructs a GetCostCenterQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetCostCentersRequest(PaginationRequest PaginationRequest);
public record GetCostCentersResponse(PaginatedResult<CostCenterDto> CostCenters);

public class GetCostCenters : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cost-centers", async ([AsParameters] PaginationRequest request, Guid CompanyId, ISender sender) =>
            {
                var result = await sender.Send(new GetCostCentersQuery(request, CompanyId));

                var response = result.Adapt<GetCostCentersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCostCenters")
            .Produces<GetCostCentersResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get list of accounting accounts")
            .WithDescription("Get list of accounting accounts");
    }
}