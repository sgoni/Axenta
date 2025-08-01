namespace Accounting.Application.Accounting.DocumentReferences.Commands.CreateDocumentReference;

public class CreateDocumentReferenceHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateDocumentReferenceCommand, CreateDocumentReferenceResult>
{
    public async Task<CreateDocumentReferenceResult> Handle(CreateDocumentReferenceCommand command,
        CancellationToken cancellationToken)
    {
        //Create documentreference entity from command object
        //Save to database
        //return result

        var documentRef = AssociateDocumentToAccountingEntry(command.DocumentReference);
        dbContext.DocumentReferences.Add(documentRef);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateDocumentReferenceResult(documentRef.Id.Value);
    }

    private DocumentReference AssociateDocumentToAccountingEntry(DocumentReferenceDto documentReferenceDto)
    {
        var newDocumentRef =
            DocumentReference.Create(
                JournalEntryId.Of(documentReferenceDto.JournalEntryId),
                documentReferenceDto.SourceType,
                SourceId.Of(documentReferenceDto.SourceId),
                documentReferenceDto.ReferenceNumber,
                documentReferenceDto.Description);

        return newDocumentRef;
    }
}