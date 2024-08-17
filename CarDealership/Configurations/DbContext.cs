using CarDealership.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Configurations
{
    public static class DbContext
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration config)
        {
            string enviroment = config.GetSection("Enviroment").Value!;
            string connectionString = config.GetConnectionString(enviroment)!;

            services.AddDbContext<CarDealershipContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
