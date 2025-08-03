namespace Accounting.API.Endpoints.JournalEntry;

//- Accepts a CreateJournalEntryRequest object.
//- Maps the request to a CreateJournalEntryCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created journalEntry's ID.

public record CreateJournalEntryRequest(JournalEntryDto JournalEntry);

public record CreateJournalEntryResponse(Guid Id);

public class CreateJournalEntry : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("journal-entries", async (CreateJournalEntryRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateJournalEntryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateJournalEntryResponse>();

                return Results.Created($"/journal-entries/{response.Id}", response);
            })
            .WithName("CreateJournalEntry")
            .Produces<CreateJournalEntryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create new seat (validating Must = Have)")
            .WithDescription("Create new seat (validating Must = Have)");
    }
}