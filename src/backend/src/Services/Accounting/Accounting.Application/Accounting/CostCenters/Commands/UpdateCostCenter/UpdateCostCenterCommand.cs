namespace Accounting.Application.Accounting.CostCenters.Commands.UpdateCostCenter;

public record UpdateCostCenterCommand(CostCenterDto CostCenter) : ICommand<UpdateCostCenterResult>;

public record UpdateCostCenterResult(bool IsSuccess);

public class UpdateCostCenterCommandValidator : AbstractValidator<UpdateCostCenterCommand>
{
    public UpdateCostCenterCommandValidator()
    {
        RuleFor(x => x.CostCenter.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
        RuleFor(x => x.CostCenter.CompanyId).NotEmpty().WithMessage("CompanyId is required.");
    }
}