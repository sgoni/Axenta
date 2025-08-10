namespace Accounting.API.Endpoints.Documentreferences;

//- Accepts a CreateDocumentReferenceRequest object.
//- Maps the request to a CreateDocumentReferenceCommand.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created account's ID.

public record CreateDocumentReferenceRequest(DocumentReferenceDto DocumentReference);

public record CreateDocumentReferenceResponse(Guid Id);

public class CreateDocumentReference : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/journal-entries/{id}/documents", async (CreateDocumentReferenceRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateDocumentReferenceCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateDocumentReferenceResponse>();

                return Results.Created($"/journal-entries/{response.Id}", response);
            })
            .WithName("CreateDocumentReference")
            .Produces<CreateDocumentReferenceResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Associate document to accounting entry")
            .WithDescription("SAssociate document to accounting entry");
    }
}