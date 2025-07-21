namespace Accounting.Application.Accounting.Accounts.Queries.GetAccoubtById;

public class GetAccountByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAccountByIdQuery, GetAccountByIdQueryResult>
{
    public async Task<GetAccountByIdQueryResult> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        // get accounts by Id using dbContext
        // return result
        var account = await dbContext.Accounts.FindAsync([query.AccountId], cancellationToken);

        if (account is null) throw new AccountNotFoundException(query.AccountId);


        return new GetAccountByIdQueryResult(account.ToAccountDto);
    }
}