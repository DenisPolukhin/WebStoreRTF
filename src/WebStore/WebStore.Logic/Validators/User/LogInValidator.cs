using FluentValidation;
using WebStore.Logic.Models.User;

namespace WebStore.Logic.Validators.User;

public class LogInValidator : AbstractValidator<LogInModel>
{
    public LogInValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .NotEmpty();
    }
}