using iText.Kernel.Exceptions;
using Parking.Service.Helpers;

namespace Parking.Service.Test.Helpers;

public class PdfStayExceptionTest
{
    [Fact(DisplayName = "PdfException - Should Set Message Correctly")]
    public void PdfException_ShouldSetMessageCorrectly()
    {
        var message = "An error occurred while generating the PDF.";

        var exception = new PdfStayException(message, null);

        Assert.Equal(message, exception.Message);
    }

    [Fact(DisplayName = "PdfException - Should Set InnerException Correctly")]
    public void PdfException_ShouldSetInnerExceptionCorrectly()
    {
        var message = "An error occurred while generating the PDF.";
        var innerExceptionMessage = "Simulated exception";
        var innerException = new Exception(innerExceptionMessage);

        var exception = new PdfStayException(message, innerException);

        Assert.Equal(innerExceptionMessage, exception.InnerException.Message);
        Assert.Equal(message, exception.Message);
    }

    [Fact(DisplayName = "PdfException - Should Be Assignable From Exception")]
    public void PdfException_ShouldBeAssignableFromException()
    {
        var message = "An error occurred while generating the PDF.";
        var innerException = new Exception("Simulated exception");

        var exception = new PdfStayException(message, innerException);

        Assert.IsAssignableFrom<Exception>(exception);
    }
}
