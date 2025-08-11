namespace Accounting.Application.Accounting.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(CompanyDto Company):ICommand<CreateCompanyResult>;
public record CreateCompanyResult(Guid CompanyId);

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Company.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(x => x.Company.CurrencyCode).NotNull().MaximumLength(50).WithMessage("CurrencyCode Id is required");
        RuleFor(x => x.Company.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
        RuleFor(x => x.Company.TaxId).NotNull().MaximumLength(150).WithMessage("Tax Id is required");
    }
}