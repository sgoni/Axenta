namespace Accounting.Application.Accounting.Periods.Commands.CreatePeriod;

public record CreatePeriodCommand() : ICommand<CreatePeriodResult>;

public record CreatePeriodResult(Guid PeriodId);