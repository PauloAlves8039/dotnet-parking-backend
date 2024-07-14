using Moq;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;

namespace Parking.Model.Test.Interfaces.Repositories;

public class RepositoryTest
{
    List<Vehicle> listVehicles = new List<Vehicle>
    {
        new Vehicle(1, "Carro", "Ford", "Ka 1.0", "Azul", 2018, "Lateral direita amassada"),
        new Vehicle(2, "Carro", "Volkswagem", "Gol", "Branco", 2016, "Farol esquerdo rachado"),
        new Vehicle(3, "Moto", "Yamaha", "FZ25 FAZER ABS", "Azul", 2022, "Possui arranhões no tanque")
    };

    [Fact(DisplayName = "GetAllAsync - Return All Entity")]
    public async Task Repository_GetAllAsync_ShouldReturnAllEntities()
    {
        var repositoryMock = new Mock<IRepository<Vehicle>>();
        repositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(listVehicles);

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(listVehicles.Count, result.Count());
        Assert.True(listVehicles.SequenceEqual(result));
    }

    [Fact(DisplayName = "GetAllAsync - Return Empty List When No Entities Exist")]
    public async Task Repository_GetAllAsync_ShouldReturnEmptyListNoEntitiesExist()
    {
        var entities = new List<Vehicle>();
        var repositoryMock = new Mock<IRepository<Vehicle>>();
        repositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(entities);

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact(DisplayName = "GetByIdAsync - Returns Entity When Id exists")]
    public async Task Repository_GetByIdAsync_ShouldReturnEntityWhenIdExists()
    {
        var entityId = 1;
        var repositoryMock = new Mock<IRepository<Vehicle>>();
        repositoryMock.Setup(repository => repository.GetByIdAsync(entityId))
            .ReturnsAsync(listVehicles.FirstOrDefault(r => r.Id == entityId));

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.GetByIdAsync(entityId);

        Assert.NotNull(result);
        Assert.Equal(listVehicles[0], result);
    }

    [Fact(DisplayName = "GetByIdAsync - Returns Entity When Id Does Not exists")]
    public async Task Repository_GetByIdAsync_ShouldReturnEntityWhenIddDoesNotExists()
    {
        var entityId = 4;
        var repositoryMock = new Mock<IRepository<Vehicle>>();
        repositoryMock.Setup(repository => repository.GetByIdAsync(entityId)).ReturnsAsync((Vehicle)null);

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.GetByIdAsync(entityId);

        Assert.Null(result);
    }

    [Fact(DisplayName = "AddAsync - And Return The Added Entity")]
    public async Task Repository_AddAsync_ShouldInsertEntityAndReturnCreatedEntity()
    {
        var simulatedId = 4;
        var newEntity = new Vehicle(simulatedId, "Moto", "Honda", "CBX 200 strada", "Vermelha", 2002, "Bom estado de conservação");

        var repositoryMock = new Mock<IRepository<Vehicle>>();

        repositoryMock.Setup(repository => repository.AddAsync(It.IsAny<Vehicle>()))
        .ReturnsAsync((Vehicle vehicle) =>
        {
            vehicle.GetType().GetProperty("Id").SetValue(vehicle, simulatedId);
            simulatedId++;
            return vehicle;
        });

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.AddAsync(newEntity); ;

        Assert.NotNull(result);
        Assert.Equal("Honda", result.Brand);
        Assert.Equal(4, result.Id);
    }

    [Fact(DisplayName = "AddAsync - Returns Null When Added Fails")]
    public async Task Repository_AddAsync_Should_ShouldReturnNullWhenInsertEntityFails()
    {
        var simulatedId = 5;
        var newEntity = new Vehicle(simulatedId, "Moto", "Honda", "CBX 200 strada", "Roxa", 2004, "Bom estado de conservação");
        var repositoryMock = new Mock<IRepository<Vehicle>>();

        repositoryMock.Setup(repository => repository.AddAsync(It.IsAny<Vehicle>())).ReturnsAsync((Vehicle)null);

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.AddAsync(newEntity);

        Assert.Null(result);
    }

    [Fact(DisplayName = "UpdateAsync - Return Updated Entity")]
    public async Task Repository_UpdateAsync_ShouldUpdateReturnUpdatedEntity()
    {
        var existingEntity = new Vehicle(1, "Carro", "Ford", "Ka 1.0", "Azul", 2018, "Lateral direita amassada");
        var updatedEntity = new Vehicle(1, "Carro", "Ford", "Ka 1.0", "Verde", 2019, "Em bom estado de conservação");

        var repositoryMock = new Mock<IRepository<Vehicle>>();

        repositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Vehicle>()))
                              .ReturnsAsync((Vehicle vehicle) => { return vehicle; });

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.UpdateAsync(updatedEntity);

        Assert.NotNull(result);
        Assert.Equal("Carro", result.VehicleType);
        Assert.Equal("Ford", result.Brand);
        Assert.Equal("Ka 1.0", result.Model);
        Assert.Equal("Verde", result.Color);
        Assert.Equal(2019, result.VehicleYear);
        Assert.Equal("Em bom estado de conservação", result.Notes);
    }

    [Fact(DisplayName = "UpdateAsync - Returns Null When Entity Not Found")]
    public async Task Repository_UpdateAsync_ShouldReturnNullWhenEntityNotFound()
    {
        var nonExistingEntity = new Vehicle(100, "Carro", "Inexistente", "Inexistente", "Verde", 2000, null);
        var repositoryMock = new Mock<IRepository<Vehicle>>();

        repositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Vehicle>())).ReturnsAsync((Vehicle)null);

        IRepository<Vehicle> _repository = repositoryMock.Object;

        var result = await _repository.UpdateAsync(nonExistingEntity);

        Assert.Null(result);
    }

    [Fact(DisplayName = "DeleteAsync - Returns The Existing Entity Delete")]
    public async Task Repository_RemoveAsync_ShouldRemoveEntityAndReturnRemovedEntity()
    {
        var entityId = 1;
        var repositoryMock = new Mock<IRepository<Vehicle>>();

        repositoryMock.Setup(repository => repository.GetByIdAsync(entityId))
            .ReturnsAsync(listVehicles.FirstOrDefault(v => v.Id == entityId));

        repositoryMock.Setup(repository => repository.DeleteAsync(It.IsAny<int>()))
        .Callback<int>(id =>
        {
            var vehicleToRemove = listVehicles.FirstOrDefault(v => v.Id == id);

            if (vehicleToRemove != null)
            {
                listVehicles.Remove(vehicleToRemove);
            }
        })
        .Returns(Task.CompletedTask);

        IRepository<Vehicle> _repository = repositoryMock.Object;

        await _repository.DeleteAsync(entityId);

        Assert.DoesNotContain(listVehicles, v => v.Id == entityId);
    }

    [Fact(DisplayName = "DeleteAsync - Does Nothing When Entity Does Not Exist")]
    public async Task Repository_DeleteAsync_ShouldDoNothingWhenEntityDoesNotExist()
    {
        var entityId = 100;
        var repositoryMock = new Mock<IRepository<Vehicle>>();

        repositoryMock.Setup(repository => repository.GetByIdAsync(entityId)).ReturnsAsync((Vehicle)null);

        repositoryMock.Setup(repository => repository.DeleteAsync(It.IsAny<int>()))
        .Callback<int>(id =>
        {
            var entityToRemove = listVehicles.FirstOrDefault(v => v.Id == id);

            if (entityToRemove != null)
            {
                listVehicles.Remove(entityToRemove);
            }

        })
        .Returns(Task.CompletedTask)
        .Verifiable();

        IRepository<Vehicle> _repository = repositoryMock.Object;

        await _repository.DeleteAsync(entityId);

        Assert.Equal(3, listVehicles.Count);
    }
}
