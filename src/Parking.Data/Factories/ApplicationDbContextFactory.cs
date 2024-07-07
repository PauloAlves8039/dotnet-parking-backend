using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Parking.Data.Context;
using Parking.Data.Factories.Interfaces;

namespace Parking.Data.Factories;

public class ApplicationDbContextFactory : IApplicationDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public ApplicationDbContextFactory(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public ApplicationDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
