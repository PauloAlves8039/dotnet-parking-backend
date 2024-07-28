using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.WebAPI.Controllers;

namespace Parking.WebAPI.Test.Controllers;

public class AddressControllerTest
{
    List<AddressDTO> listAddresses = new List<AddressDTO>
    {
        new AddressDTO(1, "Rua São Pedro", "123", "Apto 101", "Centro", "PE", "Recife", "52000-111"),
        new AddressDTO(2, "Avenida São José", "456", "Sala 203", "Boa Vista", "PE", "Recife", "52000-222"),
        new AddressDTO(3, "Praça Santa Maria", "789", "Apto 10", "Derby", "PE", "Recife", "52000-333")
    };

    [Fact(DisplayName = "GetAll - Return All Address")]
    public async Task AddressController_GetAll_ShouldReturnAllAddress()
    {
        var mockAddressService = new Mock<IAddressService>();
        mockAddressService.Setup(service => service.GetAllAsync()).ReturnsAsync(listAddresses);

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var address = Assert.IsAssignableFrom<IEnumerable<AddressDTO>>(resultOk.Value);

        Assert.Equal(listAddresses, address);
    }

    [Fact(DisplayName = "GetAll - Return Empty List When No Address Exist")]
    public async Task AddressController_GetAll_ShouldReturnEmptyListWhenNoAddressExist()
    {
        var mockAddressService = new Mock<IAddressService>();
        mockAddressService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<AddressDTO>());

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var address = Assert.IsAssignableFrom<IEnumerable<AddressDTO>>(resultOk.Value);

        Assert.Empty(address);
    }

    [Fact(DisplayName = "GetById - Returns Existing Address By Id")]
    public async Task AddressController_GetById_ShouldReturnAddressById()
    {
        var mockAddressService = new Mock<IAddressService>();
        var addressId = 1;

        var address = new AddressDTO 
        {
            Id = addressId,
            Street = "Rua São Pedro",
            Number = "123",
            Complement = "Apto 101",
            Neighborhood = "Centro",
            FederativeUnit = "PE",
            City = "Recife",
            ZipCode = "52000-111"
        };

        mockAddressService.Setup(service => service.GetByIdAsync(addressId))
            .ReturnsAsync(listAddresses.FirstOrDefault(v => v.Id == addressId));

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.GetById(addressId);

        Assert.NotNull(result);

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);

        var addressResult = Assert.IsType<AddressDTO>(resultOk.Value);

        Assert.Equal(listAddresses[0], addressResult);
    }

    [Fact(DisplayName = "GetById - Returns Address When Id Does Not exists")]
    public async Task AddressController_GetById_ShouldReturnAddressWhenIddDoesNotExists()
    {
        var mockAddressService = new Mock<IAddressService>();
        var addressId = 4;

        var address = new AddressDTO
        {
            Id = addressId,
            Street = "Rua São Pedro",
            Number = "123",
            Complement = "Apto 101",
            Neighborhood = "Centro",
            FederativeUnit = "PE",
            City = "Recife",
            ZipCode = "52000-111"
        };

        mockAddressService.Setup(service => service.GetByIdAsync(addressId)).ReturnsAsync(null as AddressDTO);

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.GetById(addressId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal($"Address with code {addressId} not found.", notFoundResult.Value);
    }

    [Fact(DisplayName = "Post - Return To Created Address")]
    public async Task AddressController_Post_ShouldReturnCreatedAddress()
    {
        var mockAddressService = new Mock<IAddressService>();

        var simulatedId = 4;
        var newAddressDTO = new AddressDTO(simulatedId, "Rua São Miguel", "893", "Apto 14", "Encruzilhada", "PE", "Recife", "52000-444");

        mockAddressService.Setup(service => service.AddAsync(newAddressDTO)).ReturnsAsync(newAddressDTO);

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.Post(newAddressDTO);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(controller.GetById), createdResult.ActionName);

        var resultVehicle = Assert.IsType<AddressDTO>(createdResult.Value);
        Assert.Equal(newAddressDTO, resultVehicle);
    }

    [Fact(DisplayName = "Post - Return BadRequest When Address Is Null")]
    public async Task AddressController_Post_ShouldReturnBadRequestWhenAddressIsNull()
    {
        var mockAddressService = new Mock<IAddressService>();
        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.Post(null);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Address data is null.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Put - Return NoContent When Address Is Updated")]
    public async Task AddressController_Put_ShouldReturnUpdatedAddress()
    {
        var mockAddressService = new Mock<IAddressService>();

        var vehicleId = 4;
        var updateAddressDTO = new AddressDTO(vehicleId, "Rua São Miguel", "753", "Apto 28", "Encruzilhada", "PE", "Recife", "52000-444");

        mockAddressService.Setup(service => service.UpdateAsync(updateAddressDTO)).ReturnsAsync(updateAddressDTO);

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.Put(vehicleId, updateAddressDTO);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockAddressService.Verify(service => service.UpdateAsync(updateAddressDTO), Times.Once);
    }

    [Fact(DisplayName = "Put - Return BadRequest When Address Ids Do Not Match")]
    public async Task AddressController_Put_ShouldReturnBadRequestWhenAddressIdsDoNotMatch()
    {
        var mockAddressService = new Mock<IAddressService>();

        var addressId = 4;
        var updateAddressDTO = new AddressDTO(5, "Rua São Miguel", "753", "Apto 28", "Encruzilhada", "PE", "Recife", "52000-444");

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.Put(addressId, updateAddressDTO);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Invalid Address data or mismatched IDs.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Delete - Return NoContent When Address Exists")]
    public async Task AddressController_Delete_ShouldReturnNoContentWhenAddressExists()
    {
        var mockAddressService = new Mock<IAddressService>();
        var addressId = 1;

        var address = new AddressDTO
        {
            Id = addressId,
            Street = "Rua São Pedro",
            Number = "123",
            Complement = "Apto 101",
            Neighborhood = "Centro",
            FederativeUnit = "PE",
            City = "Recife",
            ZipCode = "52000-111"
        };

        mockAddressService.Setup(service => service.GetByIdAsync(addressId))
            .ReturnsAsync(listAddresses.FirstOrDefault(v => v.Id == addressId));
        mockAddressService.Setup(service => service.DeleteAsync(addressId)).Returns(Task.CompletedTask);

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.Delete(addressId);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockAddressService.Verify(service => service.DeleteAsync(addressId), Times.Once);
    }

    [Fact(DisplayName = "Delete - Return NotFound When Address Does Not Exist")]
    public async Task AddressController_Delete_ShouldReturnNotFoundWhenAddressDoesNotExist()
    {
        var mockAddressService = new Mock<IAddressService>();
        var addressId = 4;

        var address = new AddressDTO
        {
            Id = addressId,
            Street = "Rua São Pedro",
            Number = "123",
            Complement = "Apto 101",
            Neighborhood = "Centro",
            FederativeUnit = "PE",
            City = "Recife",
            ZipCode = "52000-111"
        };

        mockAddressService.Setup(service => service.GetByIdAsync(addressId)).ReturnsAsync(null as AddressDTO);

        var controller = new AddressController(mockAddressService.Object);

        var result = await controller.Delete(addressId);

        var notFoundResult = Assert.IsType<NotFoundResult>(result);

        mockAddressService.Verify(service => service.DeleteAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
