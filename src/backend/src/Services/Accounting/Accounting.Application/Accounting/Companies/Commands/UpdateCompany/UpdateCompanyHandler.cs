namespace Accounting.Application.Accounting.Companies.Commands.UpdateCompany;

public class UpdateCompanyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateCompanyCommand, UpdateCompanyResult>
{
    public async Task<UpdateCompanyResult> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        //Update company entity from command object
        //save to database
        //return result

        var companyId = CompanyId.Of(command.Company.Id);
        var company = await dbContext.Companies.FindAsync([companyId], cancellationToken);

        if (company is null) throw EntityNotFoundException.For<Company>(command.Company.Id);

        UpdateCompanytWithNewValues(company, command.Company);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCompanyResult(true);
    }

    private void UpdateCompanytWithNewValues(Company company, CompanyDto commandCompany)
    {
        company.Update(
            commandCompany.Name,
            commandCompany.TaxId,
            commandCompany.Country,
            commandCompany.CurrencyCode,
            commandCompany.IsActive
        );
    }
}