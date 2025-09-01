namespace Accounting.API.Endpoints.CostCenter;

//- Accepts the cost center ID as a parameter.
//- Constructs a ActiveCostCenterCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record ActivateCostCenterRequest(Guid AccountId);
public record ActivateCostCenterResponse(bool IsSuccess);

public class ActiveCostCenter : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/cost-centers/{id}/activate", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new ActiveCostCenterCommand(Id));

                    var response = result.Adapt<ActivateCostCenterResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("ActivateCostCenter")
            .Produces<ActivateCostCenterResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Activate cost center.")
            .WithDescription("Activate cost center.");
    }
}