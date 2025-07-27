namespace Accounting.API.Endpoints;

//public record GetPeriodByIdRequest(Guid PeriodId);

public record GetPeriodByIdResponse(PeriodDto PeriodDetail);

public class GetPeriodById : ICarterModule
{
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/periods/{periodId}", async (Guid periodId, ISender sender) =>
            {
                var result = await sender.Send(new GetPeriodByIdQuery(periodId));

                var response = result.Adapt<GetPeriodByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetPeriodById")
            .Produces<GetPeriodById>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("View a specific period.")
            .WithDescription("View a specific period.");
    }
}