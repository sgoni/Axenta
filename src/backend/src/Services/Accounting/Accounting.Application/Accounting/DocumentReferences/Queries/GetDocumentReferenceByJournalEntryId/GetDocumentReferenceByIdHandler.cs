namespace Accounting.Application.Accounting.DocumentReferences.Queries.GetDocumentReferenceById;

public class GetDocumentReferenceByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetDocumentReferenceByJournalEntryIdQuery, GetDocumentReferenceByJournalEntryIdResult>
{
    public async Task<GetDocumentReferenceByJournalEntryIdResult> Handle(
        GetDocumentReferenceByJournalEntryIdQuery query, CancellationToken cancellationToken)
    {
        var documentReference = await dbContext.DocumentReferences
            .AsNoTracking()
            .Where(df => df.JournalEntryId == JournalEntryId.Of(query.JournalEntryId))
            .ToListAsync(cancellationToken);

        return new GetDocumentReferenceByJournalEntryIdResult(documentReference.ToDocumentReferenceDtoList());
    }
}