namespace Accounting.API.Endpoints.CostCenter;

//- Accepts the cost center ID as a parameter.
//- Constructs a DeactiveCostCenterCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record DeactivateCostCenterRequest(Guid AccountId);
public record DeactivateCostCenterResponse(bool IsSuccess);

public class DeactivateCostCenter : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/cost-centers/{id}/deactivate", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new DeactivateCostCenterCommand(Id));

                    var response = result.Adapt<DeactivateCostCenterResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DeactivateCostCenter")
            .Produces<DeactivateCostCenterResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deactivate cost center.")
            .WithDescription("Deactivate cost center.");
    }
}