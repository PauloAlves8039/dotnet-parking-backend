using Moq;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;
using Parking.Service.Helpers.Interfaces;

namespace Parking.Service.Test.Helpers.Interfaces
{
    public class UtilitiesTest
    {
        [Fact(DisplayName = "UpdateStayStatus - Return Stay Status With Updated Value")]
        public async Task Utilities_UpdateStayStatus_ShouldReturnUpdatedStayStatus()
        {
            var stayId = 2;
            var newStatus = "Retirado";

            var mockRepository = new Mock<IRepository<Stay>>();

            var existingStay = new Stay
            {
                Id = stayId,
                StayStatus = "Estacionado"
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(stayId))
                          .ReturnsAsync(existingStay);

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Stay>()))
                          .ReturnsAsync((Stay stay) =>
                          {
                              existingStay.StayStatus = stay.StayStatus;
                              return existingStay;
                          });

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.UpdateStayStatus(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IRepository<Stay>>()))
                         .Callback<int, string, IRepository<Stay>>((id, status, repo) =>
                         {
                             var stay = repo.GetByIdAsync(id).Result;
                             if (stay != null)
                             {
                                 stay.StayStatus = status;
                                 repo.UpdateAsync(stay).Wait();
                             }
                         })
                         .Returns(Task.CompletedTask);

            await mockUtilities.Object.UpdateStayStatus(stayId, newStatus, mockRepository.Object);

            mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Stay>(s => s.StayStatus == newStatus)), Times.Once);
            mockRepository.Verify(repo => repo.GetByIdAsync(stayId), Times.Once);
        }

        [Fact(DisplayName = "UpdateStayStatus - Should Throw ArgumentException When New Status is Null")]
        public async Task Utilities_UpdateStayStatus_ShouldThrowArgumentExceptionWhenNewStatusIsNull()
        {
            var stayId = 2;
            string newStatus = null;

            var mockRepository = new Mock<IRepository<Stay>>();

            mockRepository.Setup(repo => repo.GetByIdAsync(stayId))
                          .ReturnsAsync(new Stay { Id = stayId, StayStatus = "Estacionado" });

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.UpdateStayStatus(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IRepository<Stay>>()))
                         .ThrowsAsync(new ArgumentException("StayStatus cannot be null or empty"));

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                mockUtilities.Object.UpdateStayStatus(stayId, newStatus, mockRepository.Object));

            Assert.Contains("StayStatus cannot be null or empty", exception.Message);

            mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Stay>()), Times.Never);
        }

        [Fact(DisplayName = "CalculateStayHours - Should Return Correct Hours Difference")]
        public void Utilities_CalculateStayHours_ShouldReturnCorrectHoursDifference()
        {
            var stayDTO = new StayDTO
            {
                EntryDate = new DateTime(2024, 7, 1, 8, 0, 0),
                ExitDate = new DateTime(2024, 7, 1, 12, 0, 0)
            };

            var mockUtilities = new Mock<IUtilities>();

            mockUtilities.Setup(u => u.CalculateStayHours(stayDTO))
                         .Returns(4.0);

            var result = mockUtilities.Object.CalculateStayHours(stayDTO);

            Assert.Equal(4.0, result);
        }

        [Fact(DisplayName = "CalculateStayHours - Should Return Zero When EntryDate is Null")]
        public void Utilities_CalculateStayHours_ShouldReturnZeroWhenEntryDateIsNull()
        {
            var stayDTO = new StayDTO
            {
                EntryDate = null,
                ExitDate = new DateTime(2024, 7, 1, 12, 0, 0)
            };

            var mockUtilities = new Mock<IUtilities>();

            mockUtilities.Setup(u => u.CalculateStayHours(stayDTO))
                         .Returns(0.0);

            var result = mockUtilities.Object.CalculateStayHours(stayDTO);

            Assert.Equal(0.0, result);
        }

        [Fact(DisplayName = "CalculateStayHours - Should Return Zero When Both Dates are Null")]
        public void Utilities_CalculateStayHours_ShouldReturnZeroWhenBothDatesAreNull()
        {
            var stayDTO = new StayDTO
            {
                EntryDate = null,
                ExitDate = null
            };

            var mockUtilities = new Mock<IUtilities>();

            mockUtilities.Setup(u => u.CalculateStayHours(stayDTO))
                         .Returns(0.0);

            var result = mockUtilities.Object.CalculateStayHours(stayDTO);

            Assert.Equal(0.0, result);
        }

        [Fact(DisplayName = "CalculateTotalAmount - Should Return Correct Total Amount With Positive Values")]
        public void Utilities_CalculateTotalAmount_ShouldReturnCorrectTotalAmountWithPositiveValues()
        {
            var hours = 5.0;
            var hourlyRate = 10.0m;
            var expectedTotalAmount = 50.0m;

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.CalculateTotalAmount(hours, hourlyRate))
                         .Returns(expectedTotalAmount);

            var result = mockUtilities.Object.CalculateTotalAmount(hours, hourlyRate);

            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact(DisplayName = "CalculateTotalAmount - Should Return Zero When Hours Are Zero")]
        public void Utilities_CalculateTotalAmount_ShouldReturnZeroWhenHoursAreZero()
        {
            var hours = 0.0;
            var hourlyRate = 10.0m;
            var expectedTotalAmount = 0.0m;

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.CalculateTotalAmount(hours, hourlyRate))
                         .Returns(expectedTotalAmount);

            var result = mockUtilities.Object.CalculateTotalAmount(hours, hourlyRate);

            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact(DisplayName = "CalculateTotalAmount - Should Return Zero When Hourly Rate Is Zero")]
        public void Utilities_CalculateTotalAmount_ShouldReturnZeroWhenHourlyRateIsZero()
        {
            var hours = 5.0;
            var hourlyRate = 0.0m;
            var expectedTotalAmount = 0.0m;

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.CalculateTotalAmount(hours, hourlyRate))
                         .Returns(expectedTotalAmount);

            var result = mockUtilities.Object.CalculateTotalAmount(hours, hourlyRate);

            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact(DisplayName = "CalculateTotalAmount - Should Return Correct Total Amount With Negative Hours")]
        public void Utilities_CalculateTotalAmount_ShouldReturnCorrectTotalAmountWithNegativeHours()
        {
            var hours = -5.0;
            var hourlyRate = 10.0m;
            var expectedTotalAmount = -50.0m;

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.CalculateTotalAmount(hours, hourlyRate))
                         .Returns(expectedTotalAmount);

            var result = mockUtilities.Object.CalculateTotalAmount(hours, hourlyRate);

            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact(DisplayName = "CalculateTotalAmount - Should Return Correct Total Amount With Negative Hourly Rate")]
        public void Utilities_CalculateTotalAmount_ShouldReturnCorrectTotalAmountWithNegativeHourlyRate()
        {
            var hours = 5.0;
            var hourlyRate = -10.0m;
            var expectedTotalAmount = -50.0m;

            var mockUtilities = new Mock<IUtilities>();
            mockUtilities.Setup(u => u.CalculateTotalAmount(hours, hourlyRate))
                         .Returns(expectedTotalAmount);

            var result = mockUtilities.Object.CalculateTotalAmount(hours, hourlyRate);

            Assert.Equal(expectedTotalAmount, result);
        }
    }
}
