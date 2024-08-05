using iText.Kernel.Exceptions;
using Moq;
using Parking.Model.DTOs;
using Parking.Service.Helpers.Interfaces;

namespace Parking.Service.Test.Helpers.Interfaces;

public class PdfServiceTest
{
    [Fact(DisplayName = "GenerateStayPdf - Should Return PDF Creation")]
    public void PdfService_GenerateStayPdf_ShouldCallGenerateStayPdf()
    {
        var mockPdfService = new Mock<IPdfService>();
        var stayDto = new StayDTO
        {
            Id = 1,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockPdfService.Object.GenerateStayPdf(stayDto);
        mockPdfService.Verify(service => service.GenerateStayPdf(It.IsAny<StayDTO>()), Times.Once);
    }

    [Fact(DisplayName = "GenerateStayPdf - Should Return Exception When Creating PDF")]
    public void PdfService_GenerateStayPdf_ShouldThrowPdfException()
    {
        var mockPdfService = new Mock<IPdfService>();
        var stayDto = new StayDTO
        {
            Id = 1,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockPdfService.Setup(service => service.GenerateStayPdf(It.IsAny<StayDTO>()))
                      .Throws(new PdfException("An error occurred while generating the PDF.", new Exception("Simulated exception")));

        var exception = Assert.Throws<PdfException>(() => mockPdfService.Object.GenerateStayPdf(stayDto));
        Assert.Equal("An error occurred while generating the PDF.", exception.Message);
        Assert.Equal("Simulated exception", exception.InnerException.Message);
    }
}
