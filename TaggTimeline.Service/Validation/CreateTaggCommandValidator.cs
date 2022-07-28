
using FluentValidation;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Validation;

public class CreateTaggCommandValidator : AbstractValidator<CreateTaggCommand>
{
    public CreateTaggCommandValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty();

        RuleFor(x => x.Colour)
            .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
    }
}
