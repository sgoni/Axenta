namespace Accounting.API.Endpoints.JournalEntry;

//- Accepts a ReverseJournalEntryRequest.
//- Maps the request to an ReverseJournalEntryCommand.
//- Sends the command for processing.
//- Returns a success or error response based on the outcome.

// public record ReverseJournalEntryRequest(Guid ReversalJournalEntryId);

public record ReverseJournalEntryResponse(bool IsSuccess);

public class ReverseJournalEntry : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/journal-entries{id}/reverse", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new ReverseJournalEntryCommand(Id));

                var response = result.Adapt<ReverseJournalEntryResponse>();

                return Results.Ok(response);
            })
            .WithName("ReverseJournalEntry")
            .Produces<ReverseJournalEntryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Reverse an entry (only if the period is open)")
            .WithDescription("Reverse an entry (only if the period is open)");
    }
}