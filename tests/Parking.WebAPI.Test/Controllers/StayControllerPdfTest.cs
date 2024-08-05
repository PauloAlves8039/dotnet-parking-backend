using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.Service.Helpers.Interfaces;
using Parking.WebAPI.Controllers;

namespace Parking.WebAPI.Test.Controllers;

public class StayControllerPdfTest
{
    [Fact(DisplayName = "GeneratePdf - Returns NotFound When Stay Does Not Exist")]
    public async Task StayController_GeneratePdf_ShouldReturnNotFound_WhenStayDoesNotExist()
    {
        var mockStayService = new Mock<IStayService>();
        var mockPdfService = new Mock<IPdfService>();
        var stayId = 1;

        mockStayService.Setup(service => service.GetByIdAsync(stayId)).ReturnsAsync((StayDTO)null);

        var controller = new StayController(mockStayService.Object, mockPdfService.Object);

        var result = await controller.GeneratePdf(stayId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal($"Stay with code {stayId} not found.", notFoundResult.Value);
    }

    [Fact(DisplayName = "GeneratePdf - Returns 500 When PdfService Is Null")]
    public async Task StayController_GeneratePdf_ShouldReturn500_WhenPdfServiceIsNull()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 1;
        var stayDto = new StayDTO { Id = stayId };

        mockStayService.Setup(service => service.GetByIdAsync(stayId)).ReturnsAsync(stayDto);

        var controller = new StayController(mockStayService.Object, null);

        var result = await controller.GeneratePdf(stayId);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);

        Assert.Equal(500, statusCodeResult.StatusCode);
        Assert.Equal("PDF service is not available.", statusCodeResult.Value);
    }

    [Fact(DisplayName = "GeneratePdf - Returns Pdf File Successfully")]
    public async Task StayController_GeneratePdf_ShouldReturnPdfFile_WhenSuccessful()
    {
        var mockStayService = new Mock<IStayService>();
        var mockPdfService = new Mock<IPdfService>();
        var stayId = 1;
        var stayDto = new StayDTO { Id = stayId };
        var pdfBytes = new byte[] { 1, 2, 3 };

        mockStayService.Setup(service => service.GetByIdAsync(stayId)).ReturnsAsync(stayDto);
        mockPdfService.Setup(service => service.GenerateStayPdf(stayDto)).Returns(pdfBytes);

        var controller = new StayController(mockStayService.Object, mockPdfService.Object);

        var result = await controller.GeneratePdf(stayId);

        var fileResult = Assert.IsType<FileContentResult>(result);

        Assert.Equal("application/pdf", fileResult.ContentType);
        Assert.Equal("Stay.pdf", fileResult.FileDownloadName);
        Assert.Equal(pdfBytes, fileResult.FileContents);
    }

    [Fact(DisplayName = "GeneratePdf - Returns 500 When Exception Is Thrown")]
    public async Task StayController_GeneratePdf_ShouldReturn500_WhenExceptionIsThrown()
    {
        var mockStayService = new Mock<IStayService>();
        var mockPdfService = new Mock<IPdfService>();
        var stayId = 1;

        mockStayService.Setup(service => service.GetByIdAsync(stayId)).ThrowsAsync(new Exception("Test exception"));

        var controller = new StayController(mockStayService.Object, mockPdfService.Object);

        var result = await controller.GeneratePdf(stayId);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);

        Assert.Equal(500, statusCodeResult.StatusCode);
        Assert.Contains("Internal server error: Test exception", statusCodeResult.Value.ToString());
    }

}
