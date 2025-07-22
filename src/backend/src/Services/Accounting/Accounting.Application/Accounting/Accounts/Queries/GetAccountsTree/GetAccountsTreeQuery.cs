namespace Accounting.Application.Accounting.Accounts.Queries.GetAccountsTree;

public record GetAccountsTreeQuery : IQuery<GetAccountsTreeResult>;

public record GetAccountsTreeResult(IEnumerable<AccountTreeDto> AccountTree);