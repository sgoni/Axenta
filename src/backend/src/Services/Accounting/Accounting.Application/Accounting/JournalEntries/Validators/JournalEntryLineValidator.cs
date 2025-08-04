namespace Accounting.Application.Accounting.JournalEntries.Validators;

public class JournalEntryLineValidator : AbstractValidator<JournalEntryLineDto>
{
    public JournalEntryLineValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.Debit).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Credit).GreaterThanOrEqualTo(0);
    }
}