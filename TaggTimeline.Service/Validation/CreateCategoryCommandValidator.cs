
using FluentValidation;
using TaggTime.Service.Commands;

namespace TaggTime.Service.Validation;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty();
    }
}
