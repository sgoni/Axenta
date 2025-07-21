namespace Accounting.Application.Accounting.Accounts.Queries.GetAccoubtById;

public record GetAccountByIdQuery(Guid AccountId) : IQuery<GetAccountByIdQueryResult>;

public record GetAccountByIdQueryResult(AccountDto Account);