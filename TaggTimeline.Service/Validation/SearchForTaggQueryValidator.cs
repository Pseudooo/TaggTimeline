
using FluentValidation;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Validation;

public class SearchForTaggQueryValidator : AbstractValidator<SearchForTaggQuery>
{
    public SearchForTaggQueryValidator()
    {
        RuleFor(x => x.SearchTerm)
            .NotEmpty();
    }
}
