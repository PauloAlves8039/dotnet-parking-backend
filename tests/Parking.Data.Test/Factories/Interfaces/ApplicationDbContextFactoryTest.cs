using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Parking.Data.Context;
using Parking.Data.Factories.Interfaces;

namespace Parking.Data.Test.Factories.Interfaces
{
    public class ApplicationDbContextFactoryTest
    {
        [Fact(DisplayName = "CreateDbContext - Should Connect To The Database")]
        public void ApplicationDbContextFactory_CreateDbContext_ShouldConnectToDatabase()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var configurationMock = new Mock<IConfiguration>();
            var dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var connectionString = "Server=.\\SQLEXPRESS;Database=Parking;User Id=sa;Password=********;TrustServerCertificate=True";

            configurationMock.Setup(c => c.GetSection("ConnectionStrings")["DefaultConnection"]).Returns(connectionString);

            var dbContextFactoryMock = new Mock<IApplicationDbContextFactory>();
            dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock.Object);

            var dbContext = dbContextFactoryMock.Object.CreateDbContext();

            dbContextFactoryMock.Verify(f => f.CreateDbContext(), Times.Once, "CreateDbContext should be called once.");

            Assert.NotNull(dbContext);
        }

        [Fact(DisplayName = "CreateDbContext - Should Throw Exception When Connection String Is Missing")]
        public void ApplicationDbContextFactory_CreateDbContext_ShouldThrowExceptionWhenConnectionStringIsMissing()
        {
            var factoryMock = new Mock<IApplicationDbContextFactory>();

            factoryMock
                .Setup(f => f.CreateDbContext())
                .Throws(new InvalidOperationException("Connection string 'DefaultConnection' not found."));

            var exception = Assert.Throws<InvalidOperationException>(() => factoryMock.Object.CreateDbContext());

            Assert.Equal("Connection string 'DefaultConnection' not found.", exception.Message);
        }
    }
}
