namespace Accounting.Application.Accounting.CostCenters.Commands.CreateCostCenter;

public record CreateCostCenterCommand(CostCenterDto CostCenter) : ICommand<CreateCostCenterResult>;

public record CreateCostCenterResult(Guid Id);

public class CreateCostCenterCommandValidator : AbstractValidator<CreateCostCenterCommand>
{
    public CreateCostCenterCommandValidator()
    {
        RuleFor(x => x.CostCenter.Code).NotNull().MaximumLength(50).WithMessage("Code Id is required");
        RuleFor(x => x.CostCenter.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
        RuleFor(x => x.CostCenter.CompanyId).NotEmpty().WithMessage("CompanyId is required.");
    }
}