using Accounting.Application.Accounting.Periods.Queries.GetPeriods;

namespace Accounting.API.Endpoints;

//- Accepts pagination parameters.
//- Constructs a GetPeriodsQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetPeriodsRequest(PaginationRequest PaginationRequest);
public record GetPeriodsResponse(PaginatedResult<PeriodDto> Periods);

public class GetPeriods : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/periods", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetPeriodsQuery(request));

                var response = result.Adapt<GetPeriodsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetPeriods")
            .Produces<GetPeriodsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("List accounting periods")
            .WithDescription("List accounting periods");
    }
}