namespace Accounting.Application.Accounting.JournalEntries.Commands.CreateJournalEntry;

public record CreateJournalEntryCommand(JournalEntryDto JournalEntry) : ICommand<CreateJournalEntryResult>;

public record CreateJournalEntryResult(Guid Id);

public class CreateJournalEntryCommandValidator : AbstractValidator<CreateJournalEntryCommand>
{
    public CreateJournalEntryCommandValidator()
    {
        RuleFor(x => x.JournalEntry.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.JournalEntry.Date).NotEmpty().WithMessage("Date is required.");
        RuleFor(x => x.JournalEntry.PeriodId).NotEmpty().WithMessage("Periods is required.");
        RuleForEach(x => x.JournalEntry.Lines).SetValidator(new JournalEntryLineValidator());
        RuleFor(x => x.JournalEntry.Lines).Must(lines =>
                lines.Sum(l => l.Debit) == lines.Sum(l => l.Credit))
            .WithMessage("The seat does not square: debits ≠ credits.");
    }
}