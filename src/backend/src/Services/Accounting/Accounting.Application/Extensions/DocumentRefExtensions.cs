namespace Accounting.Application.Extensions;

public static class DocumentRefExtensions
{
    public static IEnumerable<DocumentReferenceDto> ToDocumentReferenceDtoList(
        this IEnumerable<DocumentReference> documentReferences)
    {
        return documentReferences.Select(documentReference => new DocumentReferenceDto(
            documentReference.Id.Value,
            documentReference.JournalEntryId.Value,
            documentReference.SourceType,
            documentReference.SourceId.Value,
            documentReference.ReferenceNumber,
            documentReference.Description
        ));
    }

    public static DocumentReferenceDto ToDocumentReferenceDto(this DocumentReference documentReference)
    {
        return DtoFromDocumentReference(documentReference);
    }

    public static DocumentReferenceDto DtoFromDocumentReference(DocumentReference documentReference)
    {
        return new DocumentReferenceDto(
            documentReference.Id.Value,
            documentReference.JournalEntryId.Value,
            documentReference.SourceType,
            documentReference.SourceId.Value,
            documentReference.ReferenceNumber,
            documentReference.Description
        );
    }
}