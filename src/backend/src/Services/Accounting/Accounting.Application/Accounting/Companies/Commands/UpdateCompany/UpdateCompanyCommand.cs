namespace Accounting.Application.Accounting.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand(CompanyDto Company) : ICommand<UpdateCompanyResult>;

public record UpdateCompanyResult(bool IsSuccess);

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(x => x.Company.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(x => x.Company.CurrencyCode).NotNull().MaximumLength(50).WithMessage("CurrencyCode Id is required");
        RuleFor(x => x.Company.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
        RuleFor(x => x.Company.TaxId).NotNull().MaximumLength(150).WithMessage("Tax Id is required");
    }
}