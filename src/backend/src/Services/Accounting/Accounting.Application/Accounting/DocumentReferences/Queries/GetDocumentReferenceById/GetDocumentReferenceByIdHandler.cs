namespace Accounting.Application.Accounting.DocumentReferences.Queries.GetDocumentReferenceById;

public class GetDocumentReferenceByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetDocumentReferenceByIdQuery, GetDocumentReferencesResult>
{
    public async Task<GetDocumentReferencesResult> Handle(GetDocumentReferenceByIdQuery query,
        CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Accounts.LongCountAsync(cancellationToken);

        var documentReference = await dbContext
            .DocumentReferences
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetDocumentReferencesResult(
            new PaginatedResult<DocumentReferenceDto>(
                pageIndex,
                pageSize,
                totalCount,
                documentReference.ToDocumentReferenceDtoList()));
    }
}