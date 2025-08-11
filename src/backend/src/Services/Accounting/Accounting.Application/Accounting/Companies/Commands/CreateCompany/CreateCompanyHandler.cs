namespace Accounting.Application.Accounting.Companies.Commands.CreateCompany;

public class CreateCompanyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateCompanyCommand, CreateCompanyResult>
{
    public async Task<CreateCompanyResult> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        //Create company entity from command object
        //Save to database
        //return result

        var company = CreateNewCompany(command.Company.Name, command.Company.TaxId, command.Company.Country,
            command.Company.CurrencyCode);
        dbContext.Companies.Add(company);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new CreateCompanyResult(company.Id.Value);
    }

    private Company CreateNewCompany(string name, string taxId, string country, string currencyCode)
    {
        var newCompany = Company.Create(
            CompanyId.Of(Guid.NewGuid()),
            name,
            taxId,
            country,
            currencyCode
        );

        return newCompany;
    }
}