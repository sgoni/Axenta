namespace Accounting.Application.Accounting.JournalEntries.Commands.ReverseJournalEntry;

public record ReverseJournalEntryCommand(Guid ReversalJournalEntryId) : ICommand<ReverseJournalEntryResult>;

public record ReverseJournalEntryResult(bool IsSuccess);

public class ReverseJournalEntryCommandValidator : AbstractValidator<ReverseJournalEntryCommand>
{
    public ReverseJournalEntryCommandValidator()
    {
        RuleFor(x => x.ReversalJournalEntryId).NotEmpty().WithMessage("ReversalJournalEntryId is required");
    }
}