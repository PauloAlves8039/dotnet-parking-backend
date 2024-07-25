using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Parking.Data.Context;
using Parking.Data.Factories.Interfaces;

namespace Parking.Data.Test.Factories.Interfaces
{
    public class IdentityApplicationDbContextFactoryTest
    {
        [Fact(DisplayName = "CreateDbContext - Should Connect To The Identity in Database")]
        public void IdentityApplicationDbContextFactory_CreateDbContext_ShouldConnectIdentityToDatabase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<IdentityApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var mockFactory = new Mock<IIdentityApplicationDbContextFactory>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(new IdentityApplicationDbContext(dbContextOptions));

            var context = mockFactory.Object.CreateDbContext();

            Assert.NotNull(context);
            Assert.IsType<IdentityApplicationDbContext>(context);

            context.Database.EnsureCreated();

            var user = new IdentityUser { UserName = "testuser", Email = "test@example.com" };
            context.Users.Add(user);
            context.SaveChanges();

            var fetchedUser = context.Users.FirstOrDefault(u => u.UserName == "testuser");
            Assert.NotNull(fetchedUser);
            Assert.Equal("test@example.com", fetchedUser.Email);
        }

        [Fact(DisplayName = "CreateDbContext - Should Fail To Connect When Factory Returns Null")]
        public void IdentityApplicationDbContextFactory_CreateDbContext_ShouldFailToConnectWhenFactoryReturnsNull()
        {
            var mockFactory = new Mock<IIdentityApplicationDbContextFactory>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns((IdentityApplicationDbContext)null);

            var context = mockFactory.Object.CreateDbContext();

            Assert.Null(context);
        }
    }
}
