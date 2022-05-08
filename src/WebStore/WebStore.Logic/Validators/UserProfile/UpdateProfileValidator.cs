using FluentValidation;
using WebStore.Logic.Models.UserProfile;

namespace WebStore.Logic.Validators.UserProfile;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileModel>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.BirthDate)
            .Must(IsCorrectBirthdate);
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .Unless(x => string.IsNullOrWhiteSpace(x.FirstName))
            .NotEmpty();
        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .Unless(x => string.IsNullOrWhiteSpace(x.LastName))
            .NotEmpty();
        RuleFor(x => x.MiddleName)
            .MinimumLength(2)
            .Unless(x => string.IsNullOrWhiteSpace(x.MiddleName))
            .NotEmpty();
    }

    private static bool IsCorrectBirthdate(DateTime? birthdate)
    {
        if (birthdate.HasValue)
        {
            var birthdateYear = birthdate.Value.Year;
            var currentYear = DateTime.UtcNow.Year;
            return birthdateYear <= currentYear && currentYear - birthdateYear < 120;
        }

        return true;
    }
}