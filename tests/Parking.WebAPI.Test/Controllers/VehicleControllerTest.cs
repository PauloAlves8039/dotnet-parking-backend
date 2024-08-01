using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.WebAPI.Controllers;

namespace Parking.WebAPI.Test.Controllers;

public class VehicleControllerTest
{
    List<VehicleDTO> listVehicles = new List<VehicleDTO>
    {
        new VehicleDTO(1, "Carro", "Ford", "Ka 1.0", "Azul", 2018, "Lateral direita amassada"),
        new VehicleDTO(2, "Carro", "Volkswagem", "Gol", "Branco", 2016, "Farol esquerdo rachado"),
        new VehicleDTO(3, "Moto", "Yamaha", "FZ25 FAZER ABS", "Azul", 2022, "Possui arranhões no tanque")
    };

    [Fact(DisplayName = "GetAll - Return All Vehicles")]
    public async Task VehicleController_GetAll_ShouldReturnAllVehicles()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        mockVehicleService.Setup(service => service.GetAllAsync()).ReturnsAsync(listVehicles);

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var vehicles = Assert.IsAssignableFrom<IEnumerable<VehicleDTO>>(resultOk.Value);

        Assert.Equal(listVehicles, vehicles);
    }

    [Fact(DisplayName = "GetAll - Return Empty List When No Vehicles Exist")]
    public async Task VehicleController_GetAll_ShouldReturnEmptyListWhenNoVehiclesExist()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        mockVehicleService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<VehicleDTO>());

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var vehicles = Assert.IsAssignableFrom<IEnumerable<VehicleDTO>>(resultOk.Value);

        Assert.Empty(vehicles);
    }

    [Fact(DisplayName = "GetById - Returns Existing Vehicle By Id")]
    public async Task VehicleController_GetById_ShouldReturnVehicleById()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        var vehicleId = 1;

        var address = new VehicleDTO
        {
            Id = vehicleId,
            VehicleType = "Carro",
            Brand = "Ford",
            Model = "Ka 1.0",
            Color = "Azul",
            VehicleYear = 2018,
            Notes = "Lateral direita amassada"
        };

        mockVehicleService.Setup(service => service.GetByIdAsync(vehicleId))
            .ReturnsAsync(listVehicles.FirstOrDefault(v => v.Id == vehicleId));

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.GetById(vehicleId);

        Assert.NotNull(result);

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);

        var vehicle = Assert.IsType<VehicleDTO>(resultOk.Value);

        Assert.Equal(listVehicles[0], vehicle);
    }

    [Fact(DisplayName = "GetById - Returns Vehicle When Id Does Not exists")]
    public async Task VehicleController_GetById_ShouldReturnVehicleWhenIddDoesNotExists()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        var vehicleId = 4;

        var address = new VehicleDTO
        {
            Id = vehicleId,
            VehicleType = "Carro",
            Brand = "Ford",
            Model = "Ka 1.0",
            Color = "Azul",
            VehicleYear = 2018,
            Notes = "Lateral direita amassada"
        };

        mockVehicleService.Setup(service => service.GetByIdAsync(vehicleId)).ReturnsAsync(null as VehicleDTO);

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.GetById(vehicleId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal($"Vehicle with code {vehicleId} not found.", notFoundResult.Value);
    }

    [Fact(DisplayName = "Post - Return To Created Vehicle")]
    public async Task VehicleController_Post_ShouldReturnCreatedVehicle()
    {
        var mockVehicleService = new Mock<IVehicleService>();

        var simulatedId = 4;
        var addVehicle = new VehicleDTO(simulatedId, "Moto", "Honda", "CBX 200 strada", "Vermelha", 2002, "Bom estado de conservação");

        mockVehicleService.Setup(service => service.AddAsync(addVehicle)).ReturnsAsync(addVehicle);

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.Post(addVehicle);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(controller.GetById), createdResult.ActionName);

        var resultVehicle = Assert.IsType<VehicleDTO>(createdResult.Value);
        Assert.Equal(addVehicle, resultVehicle);
    }

    [Fact(DisplayName = "Post - Return BadRequest When Vehicle Is Null")]
    public async Task VehicleController_Post_ShouldReturnBadRequestWhenVehicleIsNull()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.Post(null);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Vehicle data is null.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Put - Return NoContent When Vehicle Is Updated")]
    public async Task VehicleController_Update_ShouldReturnUpdatedVehicle()
    {
        var mockVehicleService = new Mock<IVehicleService>();

        var vehicleId = 4;
        var updateVehicle = new VehicleDTO(vehicleId, "Moto", "Honda", "CBX 200 strada", "Verde", 2002, "Bom estado de conservação");

        mockVehicleService.Setup(service => service.UpdateAsync(updateVehicle)).ReturnsAsync(updateVehicle);

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.Put(vehicleId, updateVehicle);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockVehicleService.Verify(service => service.UpdateAsync(updateVehicle), Times.Once);
    }

    [Fact(DisplayName = "Put - Return BadRequest When Vehicle Ids Do Not Match")]
    public async Task VehicleController_Update_ShouldReturnBadRequestWhenVehicleIdsDoNotMatch()
    {
        var mockVehicleService = new Mock<IVehicleService>();

        var vehicleId = 4;
        var updateVehicle = new VehicleDTO(5, "Moto", "Honda", "CBX 200 strada", "Verde", 2002, "Bom estado de conservação");

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.Put(vehicleId, updateVehicle);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Invalid vehicle data or mismatched IDs.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Delete - Return NoContent When Vehicle Exists")]
    public async Task VehicleController_Delete_ShouldReturnNoContentWhenVehicleExists()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        var vehicleId = 1;

        var deleteAddress = new VehicleDTO
        {
            Id = vehicleId,
            VehicleType = "Carro",
            Brand = "Ford",
            Model = "Ka 1.0",
            Color = "Azul",
            VehicleYear = 2018,
            Notes = "Lateral direita amassada"
        };

        mockVehicleService.Setup(service => service.GetByIdAsync(vehicleId))
            .ReturnsAsync(listVehicles.FirstOrDefault(v => v.Id == vehicleId));
        mockVehicleService.Setup(service => service.DeleteAsync(vehicleId)).Returns(Task.CompletedTask);

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.Delete(vehicleId);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockVehicleService.Verify(service => service.DeleteAsync(vehicleId), Times.Once);
    }

    [Fact(DisplayName = "Delete - Return NotFound When Vehicle Does Not Exist")]
    public async Task VehicleController_Delete_ShouldReturnNotFoundWhenVehicleDoesNotExist()
    {
        var mockVehicleService = new Mock<IVehicleService>();
        var vehicleId = 4;

        var deleteAddress = new VehicleDTO
        {
            Id = vehicleId,
            VehicleType = "Carro",
            Brand = "Ford",
            Model = "Ka 1.0",
            Color = "Azul",
            VehicleYear = 2018,
            Notes = "Lateral direita amassada"
        };

        mockVehicleService.Setup(service => service.GetByIdAsync(vehicleId)).ReturnsAsync(null as VehicleDTO);

        var controller = new VehicleController(mockVehicleService.Object);

        var result = await controller.Delete(vehicleId);

        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        
        mockVehicleService.Verify(service => service.DeleteAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
