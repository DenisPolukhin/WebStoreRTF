using FluentValidation;
using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Validators.Product;

public class CreateProductValidator : AbstractValidator<CreateProductModel>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(2)
            .NotEmpty();
        RuleFor(x => x.Description)
            .MinimumLength(10)
            .NotEmpty();
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(1);
    }
}