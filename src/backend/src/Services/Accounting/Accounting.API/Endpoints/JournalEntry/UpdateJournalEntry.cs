using Accounting.Application.Accounting.JournalEntries.Commands.UpdateJournalEntry;

namespace Accounting.API.Endpoints.JournalEntry;

//- Accepts a UpdateJournalEntryRequest.
//- Maps the request to an UpdateAccountCommand.
//- Sends the command for processing.
//- Returns a success or error response based on the outcome.

public record UpdateJournalEntryRequest(JournalEntryDto JournalEntry);

public record UpdateJournalEntryResponse(bool IsSuccess);

public class UpdateJournalEntry : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/journal-entries", async (UpdateJournalEntryRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateJournalEntryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateJournalEntryResponse>();

                return Results.Created($"/journal-entries/{response.IsSuccess}", response);
            })
            .WithName("UpdateJournalEntry")
            .Produces<UpdateJournalEntryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Modify an entry (only if the period is open)")
            .WithDescription("Modify an entry (only if the period is open)");
    }
}