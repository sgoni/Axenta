namespace Accounting.Application.Accounting.DocumentReferences.Queries.GetDocumentReferenceById;

public record GetDocumentReferenceByIdQuery(PaginationRequest PaginationRequest)
    : IQuery<GetDocumentReferencesResult>;

public record GetDocumentReferencesResult(PaginatedResult<DocumentReferenceDto> DocumentReferences);