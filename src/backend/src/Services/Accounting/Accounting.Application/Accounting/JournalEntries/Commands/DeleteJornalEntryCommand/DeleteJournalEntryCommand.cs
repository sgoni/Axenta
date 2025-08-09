namespace Accounting.Application.Accounting.JournalEntries.Commands.DeleteJornalEntryCommand;

public record DeleteJournalEntryCommand(Guid journalEntryId) : ICommand<DeleteJournalEntrytResult>;

public record DeleteJournalEntrytResult(bool IsSuccess);

public class DeleteJournalEntryCommandValidator : AbstractValidator<DeleteJournalEntryCommand>
{
    public DeleteJournalEntryCommandValidator()
    {
        RuleFor(x => x.journalEntryId).NotEmpty().WithMessage("JournalEntryId is required");
    }
}