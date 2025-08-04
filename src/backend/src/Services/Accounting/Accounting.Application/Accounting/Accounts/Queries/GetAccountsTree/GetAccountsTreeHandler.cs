namespace Accounting.Application.Accounting.Accounts.Queries.GetAccountsTree;

public class GetAccountsTreeHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAccountsTreeQuery, GetAccountsTreeResult>
{
    public async Task<GetAccountsTreeResult> Handle(GetAccountsTreeQuery query, CancellationToken cancellationToken)
    {
        // get accounts with pagination
        // return result

        var accounts = await dbContext.Accounts
            .Include(a => a.Children)
            .AsNoTracking()
            .OrderBy(account => account.Code)
            .ToListAsync(cancellationToken);

        var flatAccouts = accounts.ToAccountTreeDtoList();

        var tree = AccountExtensions.BuildTree(flatAccouts);

        return new GetAccountsTreeResult(tree);
    }
}