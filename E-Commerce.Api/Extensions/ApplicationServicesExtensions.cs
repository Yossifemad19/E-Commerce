using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Data.Identity;
using E_Commerce.Infrastructure.Services;
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

            

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<ItokenService, TokenService>();
            return services;
        }
    }
}
