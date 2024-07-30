using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.WebAPI.Controllers;

namespace Parking.WebAPI.Test.Controllers;

public class StayControllerTest
{
    List<StayDTO> listStays = new List<StayDTO>
    {
        new StayDTO
        {
            Id = 1,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        },
        new StayDTO
        {
            Id = 2,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12T15:00:00"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        },
        new StayDTO
        {
            Id = 3,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12T15:00:00"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        }
    };

    [Fact(DisplayName = "GetAll - Return All Stays")]
    public async Task StayController_GetAll_ShouldReturnAllStays()
    {
        var mockStayService = new Mock<IStayService>();
        mockStayService.Setup(service => service.GetAllAsync()).ReturnsAsync(listStays);

        var controller = new StayController(mockStayService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var stays = Assert.IsAssignableFrom<IEnumerable<StayDTO>>(resultOk.Value);

        Assert.Equal(listStays, stays);
    }

    [Fact(DisplayName = "GetAll - Return Empty List When No Stays Exist")]
    public async Task StayController_GetAll_ShouldReturnEmptyListWhenNoStaysExist()
    {
        var mockStayService = new Mock<IStayService>();
        mockStayService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<StayDTO>());

        var controller = new StayController(mockStayService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var stays = Assert.IsAssignableFrom<IEnumerable<StayDTO>>(resultOk.Value);

        Assert.Empty(stays);
    }

    [Fact(DisplayName = "GetById - Returns Existing Stay By Id")]
    public async Task StayController_GetById_ShouldReturnStayById()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 1;

        var stay = new StayDTO
        {
            Id = stayId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockStayService.Setup(service => service.GetByIdAsync(stayId))
            .ReturnsAsync(listStays.FirstOrDefault(v => v.Id == stayId));

        var controller = new StayController(mockStayService.Object);

        var result = await controller.GetById(stayId);

        Assert.NotNull(result);

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);

        var stayResult = Assert.IsType<StayDTO>(resultOk.Value);

        Assert.Equal(listStays[0], stayResult);
    }

    [Fact(DisplayName = "GetById - Returns Stay When Id Does Not exists")]
    public async Task StayController_GetById_ShouldReturnStayWhenIddDoesNotExists()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 4;

        var stay = new StayDTO
        {
            Id = stayId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockStayService.Setup(service => service.GetByIdAsync(stayId)).ReturnsAsync(null as StayDTO);

        var controller = new StayController(mockStayService.Object);

        var result = await controller.GetById(stayId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal($"Stay with code {stayId} not found.", notFoundResult.Value);
    }

    [Fact(DisplayName = "Post - Return To Created Stay")]
    public async Task StayController_Post_ShouldReturnCreatedStay()
    {
        var mockStayService = new Mock<IStayService>();
        var simulatedId = 4;

        var newStayDTO = new StayDTO
        {
            Id = simulatedId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            HourlyRate = 3.50m,
            StayStatus = "Estacionado"
        };

        mockStayService.Setup(service => service.AddAsync(newStayDTO)).ReturnsAsync(newStayDTO);

        var controller = new StayController(mockStayService.Object);

        var result = await controller.Post(newStayDTO);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(controller.GetById), createdResult.ActionName);

        var resultStay = Assert.IsType<StayDTO>(createdResult.Value);
        Assert.Equal(newStayDTO, resultStay);
    }

    [Fact(DisplayName = "Post - Return BadRequest When Stay Is Null")]
    public async Task StayController_Post_ShouldReturnBadRequestWhenStayIsNull()
    {
        var mockStayService = new Mock<IStayService>();
        var controller = new StayController(mockStayService.Object);

        var result = await controller.Post(null);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Stay data is null.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Put - Return NoContent When Stay Is Updated")]
    public async Task StayController_Update_ShouldReturnUpdatedStay()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 4;

        var updateStayDTO = new StayDTO
        {
            Id = stayId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockStayService.Setup(service => service.UpdateAsync(updateStayDTO)).ReturnsAsync(updateStayDTO);

        var controller = new StayController(mockStayService.Object);

        var result = await controller.Put(stayId, updateStayDTO);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockStayService.Verify(service => service.UpdateAsync(updateStayDTO), Times.Once);
    }

    [Fact(DisplayName = "Put - Return BadRequest When Stay Ids Do Not Match")]
    public async Task StayController_Update_ShouldReturnBadRequestWhenStaysIdsDoNotMatch()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 4;

        var updateStayDTO = new StayDTO
        {
            Id = 5,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        var controller = new StayController(mockStayService.Object);

        var result = await controller.Put(stayId, updateStayDTO);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Invalid Stay data or mismatched IDs.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Delete - Return NoContent When Stay Exists")]
    public async Task StayController_Delete_ShouldReturnNoContentWhenStayExists()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 1;

        var stay = new StayDTO
        {
            Id = stayId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockStayService.Setup(service => service.GetByIdAsync(stayId))
            .ReturnsAsync(listStays.FirstOrDefault(v => v.Id == stayId));
        mockStayService.Setup(service => service.DeleteAsync(stayId)).Returns(Task.CompletedTask);

        var controller = new StayController(mockStayService.Object);

        var result = await controller.Delete(stayId);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockStayService.Verify(service => service.DeleteAsync(stayId), Times.Once);
    }

    [Fact(DisplayName = "Delete - Return NotFound When Stay Does Not Exist")]
    public async Task StayController_Delete_ShouldReturnNotFoundWhenStayDoesNotExist()
    {
        var mockStayService = new Mock<IStayService>();
        var stayId = 5;

        var stay = new StayDTO
        {
            Id = stayId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        mockStayService.Setup(service => service.GetByIdAsync(stayId)).ReturnsAsync(null as StayDTO);

        var controller = new StayController(mockStayService.Object);

        var result = await controller.Delete(stayId);

        var notFoundResult = Assert.IsType<NotFoundResult>(result);

        mockStayService.Verify(service => service.DeleteAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
