
using FluentValidation.TestHelper;
using NUnit.Framework;
using TaggTime.Service.Commands;
using TaggTime.Service.Validation;

namespace TaggTime.Service.Test.Validation;

[TestFixture]
public class CreateCategoryCommandValidatorTests
{

    private CreateCategoryCommandValidator _validator = null!;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateCategoryCommandValidator();
    }

    [Test]
    public void Should_Error_When_Key_Empty()
    {
        var model = new CreateCategoryCommand()
        {
            Key = string.Empty,
        };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(cmd => cmd.Key);
    }

    [Test]
    public void Should_Error_When_Key_Null()
    {
        var model = new CreateCategoryCommand();
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(cmd => cmd.Key);
    }

    [Test]
    public void Should_Accept_Valid_Command()
    {
        var model = new CreateCategoryCommand()
        {
            Key = "foo",
        };
        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

}
