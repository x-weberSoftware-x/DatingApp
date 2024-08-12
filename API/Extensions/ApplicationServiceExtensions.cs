using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    //extension method, what we are extensding goes after the this keyword, then add congfig cause some of the services we wrote need the config
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        //implement our created token service, scoped services are created once per http request
        //user our ItokenService interface and the implemantation class TokenService
        //we are using an interface for abstraction and decoupling
        services.AddScoped<ITokenService, TokenService>();
        //implement our created repository
        services.AddScoped<IUserRepository, UserRepository>();
        //this will look in our current assemblies and set up all our profiles we made
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
