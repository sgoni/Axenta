namespace Accounting.Application.Accounting.Accounts.Queries.GetAccountTypes;

public class GetAccountTypesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAccountTypesQuery, GetAccountTypesResult>
{
    public async Task<GetAccountTypesResult> Handle(GetAccountTypesQuery request, CancellationToken cancellationToken)
    {
        // get account types using dbContext
        // return result

        var accountTypes = await dbContext.AccountTypes
            .AsNoTracking()
            .OrderBy(at => at.Name)
            .ToListAsync(cancellationToken);

        return new GetAccountTypesResult(accountTypes.ToAccountTypesDtoList());
    }
}