using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;

namespace Parking.Model.Test.Interfaces.Services
{
    public class DataRetrievalServiceTest
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

        [Fact(DisplayName = "GetAllAsync - Return All Records")]
        public async Task Service_GetAllAsync_ShouldReturnAllRecords()
        {
            var serviceMock = new Mock<IDataRetrievalService<StayDTO>>();

            serviceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(listStays);

            var _service = serviceMock.Object;

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(listStays.Count, result.Count());
        }

        [Fact(DisplayName = "GetAllAsync - Return Empty List When No Records Exist")]
        public async Task Service_GetAllAsync_ShouldReturnEmptyListWhenNoRecordsExist()
        {
            var serviceMock = new Mock<IDataRetrievalService<StayDTO>>();
            var emptyStays = new List<StayDTO>();

            serviceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(emptyStays);

            var _service = serviceMock.Object;

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetByIdAsync - Returns Record When Id exists")]
        public async Task Service_GetByIdAsync_ShouldReturnRecordWhenIdExists()
        {
            var serviceMock = new Mock<IDataRetrievalService<StayDTO>>();
            var stayId = 1;

            var stayDTO = new StayDTO
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

            serviceMock.Setup(service => service.GetByIdAsync(stayId)).ReturnsAsync(stayDTO);

            var _service = serviceMock.Object;

            var result = await _service.GetByIdAsync(stayId);

            Assert.NotNull(result);
            Assert.Equal(stayId, result.Id);
        }

        [Fact(DisplayName = "GetByIdAsync - Returns Null When Id Does Not Exist")]
        public async Task Service_GetByIdAsync_ShouldReturnNullWhenRecordIdDoesNotExist()
        {
            var serviceMock = new Mock<IDataRetrievalService<StayDTO>>();
            var invalidStayId = 9999;

            serviceMock.Setup(service => service.GetByIdAsync(invalidStayId)).ReturnsAsync((StayDTO)null);

            var _service = serviceMock.Object;

            var result = await _service.GetByIdAsync(invalidStayId);

            Assert.Null(result);
        }
    }
}
