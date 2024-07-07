using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parking.Data.Context;
using Parking.Data.Factories.Interfaces;

namespace Parking.Data.Factories;

public class IdentityApplicationDbContextFactory : IIdentityApplicationDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public IdentityApplicationDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IdentityApplicationDbContext CreateDbContext()
    {
        var options = _serviceProvider.GetRequiredService<DbContextOptions<IdentityApplicationDbContext>>();
        return new IdentityApplicationDbContext(options);
    }
}
