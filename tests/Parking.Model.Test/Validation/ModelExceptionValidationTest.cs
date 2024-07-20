using FluentAssertions;
using Parking.Model.Validation;

namespace Parking.Model.Test.Validation;

public class ModelExceptionValidationTest
{
    [Fact(DisplayName = "When - No Errors No Exception Thrown")]
    public void ModelExceptionValidation_When_NoErrorsNoExceptionThrown()
    {

        Action action = () => ModelExceptionValidation.When(false, "An error occurred.");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = "When - Errors Domain Exception Thrown")]
    public void ModelExceptionValidation_When_ErrorsDomainExceptionThrown()
    {

        Action action = () => ModelExceptionValidation.When(true, "An error occurred.");

        action.Should().Throw<ModelExceptionValidation>().WithMessage("An error occurred.");
    }
}
