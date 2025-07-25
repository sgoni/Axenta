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
            documentReference.SourceId.Value
        ));
    }

    public static DocumentReferenceDto DtoFromDocumentReference(this DocumentReference documentReference)
    {
        return new DocumentReferenceDto(
            documentReference.Id.Value,
            documentReference.JournalEntryId.Value,
            documentReference.SourceType,
            documentReference.SourceId.Value
        );
    }
}