namespace Accounting.Application.Accounting.Periods.Commands.OpenPeriod;

public record OpenPeriodCommand(Guid PeriodId) : ICommand<OpenPeriodResult>;

public record OpenPeriodResult(bool IsSuccess);

public class OpenPeriodCommandValidator : AbstractValidator<OpenPeriodCommand>
{
    public OpenPeriodCommandValidator()
    {
        RuleFor(x => x.PeriodId).NotEmpty().WithMessage("PeriodId is required");
    }
}