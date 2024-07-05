﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parking.Data.Context;
using Parking.Data.Repositories;
using Parking.Model.Interfaces;
using Parking.Service.Helpers.Interfaces;
using Parking.Service.Helpers;
using Parking.Service.Interfaces.Business;
using Parking.Service.Interfaces;
using Parking.Service.Mappings;
using Parking.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Parking.IoC.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
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

            services.AddScoped<IUtilities, Utilities>();
            services.AddAutoMapper(typeof(ParkingProfile));

            return services;
        }
    }
}