namespace Accounting.API.Endpoints.JournalEntry;

//- Accepts the JournalEntry ID as a parameter.
//- Constructs a DeleteJournalEntryCommand.
//- Sends the command using MediatR.
//- Returns a success or not found response.

//public record DeleteJournalEntryRequest(Guid Id);
public record DeleteJournalEntryResponse(bool IsSuccess);

public class DeleteJournalEntry : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/journal-entries/{id}", async (Guid Id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteJournalEntryCommand(Id));

                    var response = result.Adapt<DeleteJournalEntryResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DeleteJournalEntry")
            .Produces<DeleteJournalEntryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Cancel or mark as deleted.")
            .WithDescription("Cancel or mark as deleted.");
    }
}