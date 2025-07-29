namespace Accounting.API.Endpoints.Period;

//- Constructs a ClosePeriodCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record ClosePeriodRequest(Guid Period);
public record ClosePeriodResponse(bool IsSuccess);

public class ClosePeriod : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/periods/{id}/close", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new ClosePeriodCommand(Id));

                    var response = result.Adapt<ClosePeriodResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("ClosePeriod")
            .Produces<ClosePeriodResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Close period (no further movements allowed.")
            .WithDescription("Close period (no further movements allowed.");
    }
}