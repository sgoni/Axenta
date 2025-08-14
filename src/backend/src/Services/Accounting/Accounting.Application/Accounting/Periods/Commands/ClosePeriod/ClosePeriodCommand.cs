namespace Accounting.Application.Accounting.Periods.Commands.ClosePeriod;

public record ClosePeriodCommand(ClosePeriodDto Period) : ICommand<ClosePeriodResult>;

public record ClosePeriodResult(bool IsSuccess);

public class ClosePeriodCommandValidator : AbstractValidator<ClosePeriodCommand>
{
    public ClosePeriodCommandValidator()
    {
        RuleFor(x => x.Period.CompanyId).NotEmpty().WithMessage("CompanyId is required");
        RuleFor(x => x.Period.PeriodId).NotEmpty().WithMessage("PeriodId is required");
    }
}