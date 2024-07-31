using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.WebAPI.Controllers;

namespace Parking.WebAPI.Test.Controllers;

public class CustomersControllerTest
{

    List<CustomerDTO> listCustomers = new List<CustomerDTO>
    {
        new CustomerDTO(1, "Pedro dos Santos", DateOnly.ParseExact("1980-09-13", "yyyy-MM-dd"), "63179291060", "(81)988888888", "pedro@email.com", 1),
        new CustomerDTO(2, "Maria Silva", DateOnly.ParseExact("1992-04-23", "yyyy-MM-dd"), "89765432100", "(21)977777777", "maria@email.com", 2),
        new CustomerDTO(3, "João Pereira", DateOnly.ParseExact("1979-10-25", "yyyy-MM-dd"), "12345678901", "(11)966666666", "joao@email.com", 3)
    };

    [Fact(DisplayName = "GetAll - Return All Customer")]
    public async Task CustomersController_GetAll_ShouldReturnAllCustomer()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        mockCustomerService.Setup(service => service.GetAllAsync()).ReturnsAsync(listCustomers);

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var address = Assert.IsAssignableFrom<IEnumerable<CustomerDTO>>(resultOk.Value);

        Assert.Equal(listCustomers, address);
    }

    [Fact(DisplayName = "GetAll - Return Empty List When No Customer Exist")]
    public async Task CustomersController_GetAll_ShouldReturnEmptyListWhenNoCustomerExist()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        mockCustomerService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<CustomerDTO>());

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.GetAll();

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);
        var address = Assert.IsAssignableFrom<IEnumerable<CustomerDTO>>(resultOk.Value);

        Assert.Empty(address);
    }

    [Fact(DisplayName = "GetById - Returns Existing Customer By Id")]
    public async Task CustomersController_GetById_ShouldReturnCustomerById()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var customerId = 1;

        var customer = new CustomerDTO
        {
            Id = customerId,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977774444",
            AddressId = 4
        };

        mockCustomerService.Setup(service => service.GetByIdAsync(customerId))
            .ReturnsAsync(listCustomers.FirstOrDefault(v => v.Id == customerId));

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.GetById(customerId);

        Assert.NotNull(result);

        var resultOk = Assert.IsType<OkObjectResult>(result.Result);

        var customersResult = Assert.IsType<CustomerDTO>(resultOk.Value);

        Assert.Equal(listCustomers[0], customersResult);
    }

    [Fact(DisplayName = "GetById - Returns Customer When Id Does Not exists")]
    public async Task CustomersController_GetById_ShouldReturnAddressWhenIddDoesNotExists()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var customerId = 1;

        var customer = new CustomerDTO
        {
            Id = customerId,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977774444",
            AddressId = 1
        };

        mockCustomerService.Setup(service => service.GetByIdAsync(customerId)).ReturnsAsync(null as CustomerDTO);

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.GetById(customerId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal($"Customer with code {customerId} not found.", notFoundResult.Value);
    }

    [Fact(DisplayName = "Post - Return To Created Customer")]
    public async Task CustomersController_Post_ShouldReturnCreatedCustomer()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var simulatedId = 4;

        var addCustomer = new CustomerDTO
        {
            Id = simulatedId,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977774444",
            AddressId = 4
        };


        mockCustomerService.Setup(service => service.AddAsync(addCustomer)).ReturnsAsync(addCustomer);

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.Post(addCustomer);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(controller.GetById), createdResult.ActionName);

        var resultCustomer = Assert.IsType<CustomerDTO>(createdResult.Value);
        Assert.Equal(addCustomer, resultCustomer);
    }

    [Fact(DisplayName = "Post - Return BadRequest When Customer Is Null")]
    public async Task CustomersController_Post_ShouldReturnBadRequestWhenCustomerIsNull()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.Post(null);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Customer data is null.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Put - Return NoContent When Customer Is Updated")]
    public async Task CustomersController_Put_ShouldReturnUpdatedCustomer()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var customerId = 1;

        var updateCustomer = new CustomerDTO
        {
            Id = customerId,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977775555",
            AddressId = 1
        };

        mockCustomerService.Setup(service => service.UpdateAsync(updateCustomer)).ReturnsAsync(updateCustomer);

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.Put(customerId, updateCustomer);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockCustomerService.Verify(service => service.UpdateAsync(updateCustomer), Times.Once);
    }

    [Fact(DisplayName = "Put - Return BadRequest When Customer Ids Do Not Match")]
    public async Task CustomersController_Put_ShouldReturnBadRequestWhenCustomerIdsDoNotMatch()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var customerId = 1;

        var updateCustomer = new CustomerDTO
        {
            Id = 5,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977775555",
            AddressId = 1
        };

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.Put(customerId, updateCustomer);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Invalid Customer data or mismatched IDs.", badRequestResult.Value);
    }

    [Fact(DisplayName = "Delete - Return NoContent When Customer Exists")]
    public async Task CustomersController_Delete_ShouldReturnNoContentWhenCustomerExists()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var customerId = 1;

        var deleteCustomer = new CustomerDTO
        {
            Id = customerId,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977775555",
            AddressId = 1
        };

        mockCustomerService.Setup(service => service.GetByIdAsync(customerId))
            .ReturnsAsync(listCustomers.FirstOrDefault(v => v.Id == customerId));
        mockCustomerService.Setup(service => service.DeleteAsync(customerId)).Returns(Task.CompletedTask);

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.Delete(customerId);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        mockCustomerService.Verify(service => service.DeleteAsync(customerId), Times.Once);
    }

    [Fact(DisplayName = "Delete - Return NotFound When Customer Does Not Exist")]
    public async Task CustomersController_Delete_ShouldReturnNotFoundWhenCustomerDoesNotExist()
    {
        var mockCustomerService = new Mock<ICustomerService>();
        var customerId = 5;

        var deleteCustomer = new CustomerDTO
        {
            Id = customerId,
            Name = "Pedro dos Santos",
            BirthDate = DateOnly.ParseExact("18-04-1997", "dd-MM-yyyy"),
            Cpf = "43345878985",
            Phone = "(81)977775555",
            AddressId = 1
        };

        mockCustomerService.Setup(service => service.GetByIdAsync(customerId)).ReturnsAsync(null as CustomerDTO);

        var controller = new CustomersController(mockCustomerService.Object);

        var result = await controller.Delete(customerId);

        var notFoundResult = Assert.IsType<NotFoundResult>(result);

        mockCustomerService.Verify(service => service.DeleteAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
