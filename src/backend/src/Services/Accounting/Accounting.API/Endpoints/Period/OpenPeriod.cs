namespace Accounting.API.Endpoints.Period;

//- Constructs a OpenPeriodCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

public record OpenPeriodRequest(OpenPeriodDto Period);

public record OpenPeriodResponse(bool IsSuccess);

public class OpenPeriod : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/periods/open", async (OpenPeriodRequest request, ISender sender) =>
                {
                    var command = request.Adapt<OpenPeriodCommand>();

                    var result = await sender.Send(command);

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