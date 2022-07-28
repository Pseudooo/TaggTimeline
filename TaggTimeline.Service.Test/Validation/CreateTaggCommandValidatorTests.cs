
using FluentValidation.TestHelper;
using NUnit.Framework;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Validation;

namespace TaggTimeline.Service.Test.Validation;

[TestFixture]
public class CreateTaggCommandValidatorTests
{
    
    private CreateTaggCommandValidator _validator = null!;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateTaggCommandValidator();
    }

    [Test]
    public void Should_Error_When_Key_Empty()
    {
        var model = new CreateTaggCommand()
        {
            Key = string.Empty,
        };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(cmd => cmd.Key);
    }

    [Test]
    public void Should_Error_When_Key_Null()
    {
        var model = new CreateTaggCommand();
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(cmd => cmd.Key);
    }

    [Test]
    public void Should_Accept_Valid_Key()
    {
        var model = new CreateTaggCommand()
        {
            Key = "Foo",
        };
        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    
    [Test]
    public void Should_Error_With_Invalid_Colour()
    {
        var model = new CreateTaggCommand()
        {
            Key = "Foo",
            Colour = "#NotAColour"
        };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(cmd => cmd.Colour);
    }

        
    [Test]
    public void Should_Accept_Shorthand_Hex()
    {
        var model = new CreateTaggCommand()
        {
            Colour = "#FFF"
        };
        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(cmd => cmd.Colour);
    }
        
    [Test]
    public void Should_Accept_Longhand_Hex()
    {
        var model = new CreateTaggCommand()
        {
            Colour = "#CAE9F5"
        };
        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(cmd => cmd.Colour);
    }

}
