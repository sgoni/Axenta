namespace Accounting.API.Endpoints.CostCenter;

//- Accepts a UpdateCostCenterRequest object.
//- Maps the request to a UpdateCostcenterCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created account's ID.

public record UpdateCostCenterRequest(CostCenterDto CostCenter);

public record UpdateCostCenterResponse(bool IsSuccess);

public class UpdateCostCenter : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/cost-centers", async (UpdateCostCenterRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateCostCenterCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateCostCenterResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("UpdateCostCenter")
            .Produces<UpdateCostCenterResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Modify an existing cost center")
            .WithDescription("Modify an existing cost center");
    }
}