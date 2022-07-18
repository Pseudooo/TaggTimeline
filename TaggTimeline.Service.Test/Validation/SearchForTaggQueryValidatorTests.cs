
using FluentValidation.TestHelper;
using NUnit.Framework;
using TaggTimeline.Service.Queries;
using TaggTimeline.Service.Validation;

namespace TaggTimeline.Service.Test.Validation;

[TestFixture]
public class SearchForTaggQueryTests
{

    private SearchForTaggQueryValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new SearchForTaggQueryValidator();
    }

    [Test]
    public void Should_Error_When_SearchTerm_Empty()
    {
        var model = new SearchForTaggQuery()
        {
            SearchTerm = string.Empty,
        };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(query => query.SearchTerm);
    }

    [Test]
    public void Should_Error_When_SearchTerm_Null()
    {
        var model = new SearchForTaggQuery();
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(query => query.SearchTerm);
    }

    [Test]
    public void Should_Accept_Valid_SearchTerm()
    {
        var model = new SearchForTaggQuery()
        {
            SearchTerm = "Foo",
        };
        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

}
