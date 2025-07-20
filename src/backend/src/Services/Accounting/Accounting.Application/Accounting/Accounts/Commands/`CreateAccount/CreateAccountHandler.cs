namespace Accounting.Application.Accounting.Accounts.Commands._CreateAccount;

public class CreateAccountHandler : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    public Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        //Create account entity from command object
        //Save to database
        //return result

        throw new NotImplementedException();
    }
}