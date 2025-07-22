namespace Accounting.Application.Accounting.Accounts.Queries.GetAccountsTree;

public class GetAccountsTreeHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAccountsTreeQuery, GetAccountsTreeResult>
{
    public async Task<GetAccountsTreeResult> Handle(GetAccountsTreeQuery query, CancellationToken cancellationToken)
    {
        var accounts = await dbContext.Accounts
            .Include(a => a.Children)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var flatAccouts = accounts.ToAccountTreeDtoList();
        var tree = AccountExtensions.AccountTreeDto(flatAccouts);

        return new GetAccountsTreeResult(tree);
    }
}