﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Parking.Data.Context;
using System.Text;

namespace Parking.IoC.Configurations
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(
                    JwtBearerDefaults.AuthenticationScheme).
                    AddJwtBearer(options =>
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidAudience = configuration["TokenConfiguration:Audience"],
                         ValidIssuer = configuration["TokenConfiguration:Issuer"],
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(configuration["Jwt:key"]))

                     });

            return services;
        }
    }
}
