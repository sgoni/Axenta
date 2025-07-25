namespace Accounting.Application.Accounting.DocumentReferences.Commands.CreateDocumentReference;

public record CreateDocumentReferenceCommand(DocumentReferenceDto documentRefDetail)
    : ICommand<CreateDocumentReferenceResult>;

public record CreateDocumentReferenceResult(Guid Id);