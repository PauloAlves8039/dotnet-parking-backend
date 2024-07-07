using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Parking.IoC.Configurations
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services) 
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Parking System",
                    Version = "v1",
                    Description = "Project to manipulate a simple Parking System.",
                    Contact = new OpenApiContact
                    {
                        Name = "Paulo Alves",
                        Email = "pj38alves@gmail.com",
                        Url = new Uri("https://github.com/PauloAlves8039"),
                    },
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
                    "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}
