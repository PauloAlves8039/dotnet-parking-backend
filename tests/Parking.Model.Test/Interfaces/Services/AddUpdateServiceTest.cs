using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;

namespace Parking.Model.Test.Interfaces.Services;

public class AddUpdateServiceTest
{
    [Fact(DisplayName = "AddAsync - Return The Added Record")]
    public async Task Service_AddAsync_ShouldInsertRecordAndReturnAddedRecord()
    {
        var serviceMock = new Mock<IAddUpdateService<StayDTO>>();
        var simulatedId = 1;

        var addStay = new StayDTO
        {
            Id = simulatedId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            HourlyRate = 3.50m,
            StayStatus = "Estacionado"
        };

        serviceMock.Setup(service => service.AddAsync(addStay)).Verifiable();

        var _service = serviceMock.Object;

        await _service.AddAsync(addStay);

        serviceMock.Verify();
    }

    [Fact(DisplayName = "AddAsync - Returns Null When Add Fails")]
    public async Task Service_AddAsync_ShouldReturnNullWhenAddedRecordFails()
    {
        var serviceMock = new Mock<IAddUpdateService<StayDTO>>();
        var simulatedId = 1;

        var addStay = new StayDTO
        {
            Id = simulatedId,
            CustomerVehicleId = 1,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            HourlyRate = 3.50m,
            StayStatus = "Estacionado"
        };

        serviceMock.Setup(service => service.AddAsync(addStay)).ThrowsAsync(new ArgumentException("Invalid Stay Added"));

        var _service = serviceMock.Object;

        Func<Task> act = async () => await _service.AddAsync(addStay);

        await Assert.ThrowsAsync<ArgumentException>(act);
        serviceMock.Verify(service => service.AddAsync(addStay), Times.Once);
    }

    [Fact(DisplayName = "UpdateAsync - Return Updated Record")]
    public async Task Service_UpdateAsync_ShouldUpdateReturnUpdatedRecord()
    {
        var serviceMock = new Mock<IAddUpdateService<StayDTO>>();

        var updateStay = new StayDTO
        {
            Id = 1,
            CustomerVehicleId = 2,
            LicensePlate = "XML123",
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        serviceMock.Setup(service => service.UpdateAsync(updateStay)).Verifiable();

        var _service = serviceMock.Object;

        await _service.UpdateAsync(updateStay);

        serviceMock.Verify();
    }

    [Fact(DisplayName = "UpdateAsync - Returns Null When Record Not Found")]
    public async Task Service_UpdateAsync_ShouldReturnNullWhenRecordNotFound()
    {
        var serviceMock = new Mock<IAddUpdateService<StayDTO>>();

        var updateStay = new StayDTO
        {
            Id = 1,
            CustomerVehicleId = 2,
            LicensePlate = null,
            EntryDate = DateTime.Parse("2024-07-12 15:00:00.000"),
            ExitDate = DateTime.Parse("2024-07-12 21:15:43.713"),
            HourlyRate = 3.50m,
            TotalAmount = 21.92m,
            StayStatus = "Retirado"
        };

        serviceMock.Setup(service => service.UpdateAsync(updateStay))
            .ThrowsAsync(new ArgumentException("Invalid Stay update"));

        var _service = serviceMock.Object;

        Func<Task> act = async () => await _service.UpdateAsync(updateStay);

        await Assert.ThrowsAsync<ArgumentException>(act);
        serviceMock.Verify(service => service.UpdateAsync(updateStay), Times.Once);
    }
}
