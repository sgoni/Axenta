namespace Accounting.Application.Accounting.Accounts.Queries.GetAccounts;

public class GetAccountsHandler(IApplicationDbContext dbContext) : IQueryHandler<GetAccountsQuery, GetAccountsResult>
{
    public async Task<GetAccountsResult> Handle(GetAccountsQuery query, CancellationToken cancellationToken)
    {
        // get accounts with pagination
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Accounts.LongCountAsync(cancellationToken);

        var accounts = await dbContext.Accounts
            .AsNoTracking()
            .OrderBy(account => account.Id.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetAccountsResult(
            new PaginatedResult<AccountDto>(
                pageIndex,
                pageSize,
                totalCount,
                accounts.ToAccountDtoList()));
    }
}