namespace Accounting.API.Endpoints.CostCenter;

//- Accepts a CreateCostCenterRequest object.
//- Maps the request to a CreateAccountCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created account's ID.

public record CreateCostCenterRequest(CostCenterDto CostCenter);

public record CreateCostCenterResponse(Guid Id);

public class CreateCostcenter : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cost-centers", async (CreateCostCenterRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateCostCenterCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateCostCenterResponse>();

                    return Results.Created($"/accounts/{response.Id}", response);
                }
            )
            .WithName("CreateCostCenter")
            .Produces<CreateCostCenterResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new cost center")
            .WithDescription("Create a new cost center");
    }
}