using api.Helpers;
using api.Interfaces;
using api.Services;
using Microsoft.EntityFrameworkCore;
using VZAggregator.Data;
using VZAggregator.Data.Repositories;
using VZAggregator.Helpers;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;
using VZAggregator.Services;

namespace VZAggregator.Models.Extensions
{
    public static class ApplicationServiceExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAddressesService, AddressesService>();
            services.AddScoped<IAddressesRepository, AddressesRepository>();
            services.AddScoped<ICarriersService, CarriersService>();
            services.AddScoped<ICarriersRepository, CarriersRepository>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<ITransportsService, TransportsService>();
            services.AddScoped<ITransportsRepository, TransportsRepository>();
            services.AddScoped<ITripsService, TripsService>();
            services.AddScoped<ITripsRepository, TripsRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<LogUserActivity>();
            services.AddDbContext<DataContext>(options=>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }   
    } 
}