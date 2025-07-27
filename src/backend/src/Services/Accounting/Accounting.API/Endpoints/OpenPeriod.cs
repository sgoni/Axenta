namespace Accounting.API.Endpoints;

//- Constructs a OpenPeriodCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record OpenPeriodRequest(Guid Period);
public record OpenPeriodResponse(bool IsSuccess);

public class OpenPeriod : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/periods/{id}/open", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new OpenPeriodCommand(Id));

                    var response = result.Adapt<OpenPeriodResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("OpenPeriod")
            .Produces<OpenPeriodResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Reopen a closed period (if applicable.")
            .WithDescription("Reopen a closed period (if applicable).");
    }
}