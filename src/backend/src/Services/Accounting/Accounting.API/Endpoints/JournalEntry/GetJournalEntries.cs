namespace Accounting.API.Endpoints.JournalEntry;

//- Accepts pagination parameters.
//- Constructs a GetJournalEntriesQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetJournalEntriesRequest(PaginationRequest PaginationRequest);

public record GetJournalEntriesResponse(PaginatedResult<JournalEntryDto> JournalEntries);

public class GetJournalEntries : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journal-entries",
                async ([AsParameters] PaginationRequest request, Guid PeriodId, Guid CompanyId, ISender sender) =>
                {
                    var result =
                        await sender.Send(new GetJournalEntriesQuery(request, PeriodId, CompanyId));

                    var response = result.Adapt<GetJournalEntriesResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetJournalEntries")
            .Produces<GetJournalEntriesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("List countable seats")
            .WithDescription("List countable seats");
    }
}