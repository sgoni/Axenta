namespace Accounting.Application.Accounting.JournalEntries.Commands.UpdateJournalEntry;

public record UpdateJournalEntryCommand(JournalEntryDto JournalEntry) : ICommand<UpdateJournalEntryResult>;

public record UpdateJournalEntryResult(bool IsSuccess);

public class UpdateJournalEntryCommandValidator : AbstractValidator<UpdateJournalEntryCommand>
{
    public UpdateJournalEntryCommandValidator()
    {
        RuleFor(x => x.JournalEntry.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.JournalEntry.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.JournalEntry.PeriodId).NotEmpty().WithMessage("Periods is required.");
    }
}