
using FluentValidation;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Validation;

public class CreateTaggCommandValidator : AbstractValidator<CreateTaggCommand>
{
    public CreateTaggCommandValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty();
    }
}