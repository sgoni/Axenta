namespace Accounting.Application.Accounting.Periods.Commands.CreatePeriod;

public record CreatePeriodCommand(CreatePeriodDto Period) : ICommand<CreatePeriodResult>;

public record CreatePeriodResult(Guid PeriodId);

public class CreatePeriodCommandValidator : AbstractValidator<CreatePeriodCommand>
{
    public CreatePeriodCommandValidator()
    {
        RuleFor(x => x.Period.CompanyId).NotEmpty().WithMessage("CompanyId is required");
    }
}