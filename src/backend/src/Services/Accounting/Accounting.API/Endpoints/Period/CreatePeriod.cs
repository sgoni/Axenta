namespace Accounting.API.Endpoints.Period;

public record CreatePeriodRequest;

public record CreatePeriodResponse(Guid PeriodId);

public class CreatePeriod : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/periods", async (CreatePeriodRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreatePeriodCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreatePeriodResponse>();

                    return Results.Created($"/periods/{response.PeriodId}", response);
                }
            )
            .WithName("CreatePeriod")
            .Produces<CreatePeriodResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithSummary("Create accounting period")
            .WithDescription("Create accounting period");
    }
}