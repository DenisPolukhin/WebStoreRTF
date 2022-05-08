using FluentValidation;
using WebStore.Common.Models;

namespace WebStore.Logic.Validators.Page;

public class PageModelValidator : AbstractValidator<PageModel>
{
    public PageModelValidator()
    {
        RuleFor(x => x.Size)
            .GreaterThanOrEqualTo(18);
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);
    }
}