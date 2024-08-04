using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parking.Data.Context;
using Parking.Service.Helpers.Interfaces;
using Parking.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Parking.IoC.Mappings;
using Parking.Model.Interfaces.Repositories;
using Parking.Data.Repositories.Implementations;
using Parking.Model.Interfaces.Services;
using Parking.Service.Services.Implementations;

namespace Parking.IoC.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<IdentityApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IStayRepository, StayRepository>();
        services.AddScoped<ICustomerVehicleRepository, CustomerVehicleRepository>();

        services.AddScoped(typeof(IService<,>), typeof(Service<,>));
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IStayService, StayService>();
        services.AddScoped<ICustomerVehicleService, CustomerVehicleService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUtilities, Utilities>();
        services.AddAutoMapper(typeof(ParkingProfile));

        services.AddScoped<IPdfService, PdfService>();

        return services;
    }
}
