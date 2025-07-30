namespace Accounting.Application.Accounting.DocumentReferences.Commands.CreateDocumentReference;

public record CreateDocumentReferenceCommand(DocumentReferenceDto DocumentReference)
    : ICommand<CreateDocumentReferenceResult>;

public record CreateDocumentReferenceResult(Guid Id);