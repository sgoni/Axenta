namespace Accounting.Application.Accounting.Accounts.Queries.GetAccounts;

public record GetAccountsQuery(PaginationRequest PaginationRequest) : IQuery<GetAccountsResult>;

public record GetAccountsResult(PaginatedResult<AccountDto> Accounts);