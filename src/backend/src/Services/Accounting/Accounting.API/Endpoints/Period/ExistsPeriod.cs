namespace Accounting.API.Endpoints.Period;

//public record ExistsPeriodRequest(int year, int Month);

public record ExistsPeriodResponse(bool Exists);

public class ExistsPeriod : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/periods/year={year}&month={month}", async (int year, int month, ISender sender) =>
            {
                var result = await sender.Send(new GetPeriodExistQuery(year, month));

                var response = result.Adapt<ExistsPeriodResponse>();

                return Results.Ok(response);
            })
            .WithName("ExistsPeriod")
            .Produces<ExistsPeriod>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("The Period Already Exists.")
            .WithDescription("The Period Already Exists.");
    }
}