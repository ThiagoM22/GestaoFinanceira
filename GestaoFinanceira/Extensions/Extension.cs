using GestaoFinanceira.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinanceira.Extensions
{
    public static class Extension
    {
        // Extensions/DatabaseExtensions.cs
            public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddDbContext<ApiDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                return services;
            }
        
    }
}
