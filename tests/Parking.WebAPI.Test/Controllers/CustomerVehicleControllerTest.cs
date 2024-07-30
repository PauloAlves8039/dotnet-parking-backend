using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.WebAPI.Controllers;


namespace Parking.WebAPI.Test.Controllers
{
    public class CustomerVehicleControllerTest
    {
        List<CustomerVehicleDTO> listCustomerVehicles = new List<CustomerVehicleDTO>
        {
            new CustomerVehicleDTO(1, 1, 1),
            new CustomerVehicleDTO(2, 2, 2),
            new CustomerVehicleDTO(3, 3, 3)
        };

        [Fact(DisplayName = "GetAll - Return All Customer Vehicle")]
        public async Task CustomerVehicleController_GetAll_ShouldReturnAllCustomerVehicle()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();
            mockCustomerVehicleService.Setup(service => service.GetAllAsync()).ReturnsAsync(listCustomerVehicles);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.GetAll();

            var resultOk = Assert.IsType<OkObjectResult>(result.Result);
            var customersVehicles = Assert.IsAssignableFrom<IEnumerable<CustomerVehicleDTO>>(resultOk.Value);

            Assert.Equal(listCustomerVehicles, customersVehicles);
        }

        [Fact(DisplayName = "GetAll - Return Empty List When No Customer Vehicles Exist")]
        public async Task CustomerVehicleController_GetAll_ShouldReturnEmptyListWhenNoCustomerVehiclesExist()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();
            mockCustomerVehicleService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<CustomerVehicleDTO>());

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.GetAll();

            var resultOk = Assert.IsType<OkObjectResult>(result.Result);
            var customersVehicles = Assert.IsAssignableFrom<IEnumerable<CustomerVehicleDTO>>(resultOk.Value);

            Assert.Empty(customersVehicles);
        }

        [Fact(DisplayName = "GetById - Returns Existing Customer Vehicle By Id")]
        public async Task CustomerVehicleController_GetById_ShouldReturnCustomerVehicleById()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();
            var customerVehicleId = 1;

            var customerVehicle = new CustomerVehicleDTO
            {
                Id = customerVehicleId,
                CustomerId = 1,
                VehicleId = 1
            };

            mockCustomerVehicleService.Setup(service => service.GetByIdAsync(customerVehicleId))
                .ReturnsAsync(listCustomerVehicles.FirstOrDefault(v => v.Id == customerVehicleId));

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.GetById(customerVehicleId);

            Assert.NotNull(result);

            var resultOk = Assert.IsType<OkObjectResult>(result.Result);

            var customerVehicleResult = Assert.IsType<CustomerVehicleDTO>(resultOk.Value);

            Assert.Equal(listCustomerVehicles[0], customerVehicleResult);
        }

        [Fact(DisplayName = "GetById - Returns Customer Vehicle When Id Does Not exists")]
        public async Task CustomerVehicleController_GetById_ShouldReturnCustomerVehicleWhenIddDoesNotExists()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();
            var customerVehicleId = 4;

            var customerVehicle = new CustomerVehicleDTO
            {
                Id = customerVehicleId,
                CustomerId = 1,
                VehicleId = 1
            };

            mockCustomerVehicleService.Setup(service => service.GetByIdAsync(customerVehicleId)).ReturnsAsync(null as CustomerVehicleDTO);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.GetById(customerVehicleId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal($"Customer Vehicle with code {customerVehicleId} not found.", notFoundResult.Value);
        }

        [Fact(DisplayName = "Post - Return To Created Customer Vehicle")]
        public async Task CustomerVehicleController_Post_ShouldReturnCreatedCustomerVehicle()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();

            var simulatedId = 4;
            var newCustomerVehicleDTO = new CustomerVehicleDTO(simulatedId, 4, 4);

            mockCustomerVehicleService.Setup(service => service.AddAsync(newCustomerVehicleDTO)).ReturnsAsync(newCustomerVehicleDTO);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.Post(newCustomerVehicleDTO);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Equal(nameof(controller.GetById), createdResult.ActionName);

            var resultCustomerVehicle = Assert.IsType<CustomerVehicleDTO>(createdResult.Value);
            Assert.Equal(newCustomerVehicleDTO, resultCustomerVehicle);
        }

        [Fact(DisplayName = "Post - Return BadRequest When Customer Vehicle Is Null")]
        public async Task CustomerVehicleController_Post_ShouldReturnBadRequestWhenCustomerVehicleIsNull()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();
            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.Post(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Customer Vehicle data is null.", badRequestResult.Value);
        }

        [Fact(DisplayName = "Put - Return NoContent When Customer Vehicle Is Updated")]
        public async Task CustomerVehicleController_Update_ShouldReturnUpdatedCustomerVehicle()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();

            var customerVehicleId = 4;
            var updateCustomerVehicleDTO = new CustomerVehicleDTO(customerVehicleId, 5, 5);

            mockCustomerVehicleService.Setup(service => service.UpdateAsync(updateCustomerVehicleDTO)).ReturnsAsync(updateCustomerVehicleDTO);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.Put(customerVehicleId, updateCustomerVehicleDTO);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            mockCustomerVehicleService.Verify(service => service.UpdateAsync(updateCustomerVehicleDTO), Times.Once);
        }

        [Fact(DisplayName = "Put - Return BadRequest When Customer Vehicle Ids Do Not Match")]
        public async Task CustomerVehicleController_Update_ShouldReturnBadRequestWhenCustomerVehicleIdsDoNotMatch()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();

            var customerVehicleId = 5;
            var updateCustomerVehicleDTO = new CustomerVehicleDTO(1, 4, 4);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.Put(customerVehicleId, updateCustomerVehicleDTO);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Invalid Customer Vehicle data or mismatched IDs.", badRequestResult.Value);
        }

        [Fact(DisplayName = "Delete - Return NoContent When Customer Vehicle Exists")]
        public async Task CustomerVehicleController_Delete_ShouldReturnNoContentWhenCustomerVehicleExists()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();

            var customerVehicleId = 2;
            var deleteCustomerVehicleDTO = new CustomerVehicleDTO(customerVehicleId, 2, 2);

            mockCustomerVehicleService.Setup(service => service.GetByIdAsync(customerVehicleId))
                .ReturnsAsync(listCustomerVehicles.FirstOrDefault(v => v.Id == customerVehicleId));
            mockCustomerVehicleService.Setup(service => service.DeleteAsync(customerVehicleId)).Returns(Task.CompletedTask);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.Delete(customerVehicleId);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            mockCustomerVehicleService.Verify(service => service.DeleteAsync(customerVehicleId), Times.Once);
        }

        [Fact(DisplayName = "Delete - Return NotFound When Customer Vehicle Does Not Exist")]
        public async Task CustomerVehicleController_Delete_ShouldReturnNotFoundWhenCustomerVehicleDoesNotExist()
        {
            var mockCustomerVehicleService = new Mock<ICustomerVehicleService>();

            var customerVehicleId = 4;
            var deleteCustomerVehicleDTO = new CustomerVehicleDTO(customerVehicleId, 2, 2);

            mockCustomerVehicleService.Setup(service => service.GetByIdAsync(customerVehicleId)).ReturnsAsync(null as CustomerVehicleDTO);

            var controller = new CustomerVehicleController(mockCustomerVehicleService.Object);

            var result = await controller.Delete(customerVehicleId);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);

            mockCustomerVehicleService.Verify(service => service.DeleteAsync(It.IsAny<int>()), Times.Never);

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

    }
}
