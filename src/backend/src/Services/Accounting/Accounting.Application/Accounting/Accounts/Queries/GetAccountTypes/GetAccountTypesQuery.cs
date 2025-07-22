namespace Accounting.Application.Accounting.Accounts.Queries.GetAccountTypes;

public record GetAccountTypesQuery() : IQuery<GetAccountTypesResult>;

public record GetAccountTypesResult(IEnumerable<AccountTypeDto> AccountTypes);