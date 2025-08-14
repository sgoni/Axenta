namespace Accounting.API.Endpoints.Period;

//- Constructs a ClosePeriodCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

public record ClosePeriodRequest(ClosePeriodDto Period);

public record ClosePeriodResponse(bool IsSuccess);

public class ClosePeriod : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/periods/close", async (ClosePeriodRequest request, ISender sender) =>
                {
                    var command = request.Adapt<ClosePeriodCommand>();

                    var result = await sender.Send(command);

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