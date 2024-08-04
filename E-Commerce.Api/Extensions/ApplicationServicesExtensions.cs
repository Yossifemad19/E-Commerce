using E_Commerce.Infrastructure.Data;
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

            //services.
            return services;
        }
    }
}
