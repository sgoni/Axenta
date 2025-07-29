namespace Accounting.API.Endpoints.Documentreferences;

//public record GetDocumentReferenceByJournalEntryIdRequest(Guid JournalEntryId);

public record GetDocumentReferenceByJournalEntryIdResponse(DocumentReferenceDto documentReferenceDetail);

public class GetDocumentById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journal-entries/{id}/documents", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetDocumentReferenceByJournalEntryIdQuery(id));

                var response = result.Adapt<GetDocumentReferenceByJournalEntryIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetDocumentReferenceByJournalEntryId")
            .Produces<GetDocumentReferenceByJournalEntryIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("See referenced documents.")
            .WithDescription("See referenced documents.");
    }
}