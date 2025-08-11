namespace Accounting.API.Endpoints.JournalEntry;

//- Accepts a JournalEntry ID.
//- Uses a GetJournalEntryByIdQuery to fetch orders.
//- Returns JournalEntry for thar Id.

//public record GetJournalEntryByIdRequest(Guid Id);

public record GetJournalEntryByIdResponse(JournalEntryDto JournalEntryDetail);

public class GetJournalEntryById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journal-entries/{journalEntryId}", async (Guid journalEntryId, ISender sender) =>
            {
                var result = await sender.Send(new GetJournalEntryByIdQuery(journalEntryId));

                var response = result.Adapt<GetJournalEntryByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetJournalEntryById")
            .Produces<GetJournalEntryByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("View detail of a seat.")
            .WithDescription("View detail of a seat.");
    }
}