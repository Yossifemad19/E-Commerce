using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Data.Identity;
using E_Commerce.Infrastructure.Services;
using StackExchange.Redis;

using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<StoreContext>(
                options => options.UseSqlServer(config.GetConnectionString("default"))
                );

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"),true);

                return ConnectionMultiplexer.Connect(configuration);
            });


            services.AddScoped<IBasketService,BasketService>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<ItokenService, TokenService>();
            return services;
        }
    }
}
