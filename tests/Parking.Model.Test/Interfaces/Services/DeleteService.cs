using Moq;
using Parking.Model.Interfaces.Services;

namespace Parking.Model.Test.Interfaces.Services
{
    public class DeleteService
    {
        [Fact(DisplayName = "DeleteAsync - Returns The Existing Record Delete")]
        public async Task Service_DeleteAsync_ShouldRemoveRecordAndReturnRemovedRecord()
        {
            var serviceMock = new Mock<IDeleteService>();

            var stayId = 1;

            serviceMock.Setup(service => service.DeleteAsync(stayId)).Verifiable();

            var _service = serviceMock.Object;

            await _service.DeleteAsync(stayId);

            serviceMock.Verify();
        }

        [Fact(DisplayName = "DeleteAsync - Returns Null When Record Not Found")]
        public async Task Service_DeleteAsync_ShouldReturnNullWhenRecordNotFound()
        {
            var serviceMock = new Mock<IDeleteService>();

            var invalidStayId = 9999;

            serviceMock.Setup(service => service.DeleteAsync(invalidStayId))
                .ThrowsAsync(new InvalidOperationException("Invalid stay removal"));

            var _service = serviceMock.Object;

            Func<Task> act = async () => await _service.DeleteAsync(invalidStayId);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
