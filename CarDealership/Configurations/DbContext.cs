using Microsoft.EntityFrameworkCore;
using RealStateAgency.Models;

namespace RealStateAgency.Configurations
{
    public static class DbContext
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration config)
        {
            string enviroment = config.GetSection("Enviroment").Value!;
            string connectionString = config.GetConnectionString(enviroment)!;

            services.AddDbContext<RealStateAgencyContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
