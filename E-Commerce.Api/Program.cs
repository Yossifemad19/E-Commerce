
using E_Commerce.Api.Errors;
using E_Commerce.Api.Extensions;
using E_Commerce.Api.Middlewares;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Data.Identity;
using E_Commerce.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .SelectMany(e => e.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                var errorResponse = new ApiValidation
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
                
            }
            ) ;

            builder.Services.addApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Erros/{0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using var scope=app.Services.CreateScope(); 
            var services=scope.ServiceProvider;
            var context=services.GetRequiredService<StoreContext>();
            var logger =services.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedDataAsync(context);

                var userManager=services.GetRequiredService<UserManager<AppUser>>();
                var identityContext= services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedIdentityDateAsync(userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during migration");
            }

            app.Run();
        }
    }
}
