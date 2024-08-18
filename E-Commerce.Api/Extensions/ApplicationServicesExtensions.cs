using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Data.Identity;
using E_Commerce.Infrastructure.Services;
using StackExchange.Redis;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

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


            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped(typeof(IUnitOfWork<>),typeof(GenericRepository<>));
            services.AddScoped<ItokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();      
            services.AddScoped<IOrderService,OrderService>();  
            services.AddScoped<IPaymentService,PaymentService>();

            services.AddSingleton<ICachedService,CachedService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
