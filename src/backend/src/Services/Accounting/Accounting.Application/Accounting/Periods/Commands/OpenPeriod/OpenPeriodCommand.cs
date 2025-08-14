namespace Accounting.Application.Accounting.Periods.Commands.OpenPeriod;

public record OpenPeriodCommand(OpenPeriodDto Period) : ICommand<OpenPeriodResult>;

public record OpenPeriodResult(bool IsSuccess);

public class OpenPeriodCommandValidator : AbstractValidator<OpenPeriodCommand>
{
    public OpenPeriodCommandValidator()
    {
        RuleFor(x => x.Period.CompanyId).NotEmpty().WithMessage("CompanyId is required");
        RuleFor(x => x.Period.PeriodId).NotEmpty().WithMessage("PeriodId is required");
    }
}