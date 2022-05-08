using FluentValidation;
using WebStore.Logic.Models.User;

namespace WebStore.Logic.Validators.User;

public class SignUpValidator : AbstractValidator<SignUpModel>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .NotEmpty();
    }
}