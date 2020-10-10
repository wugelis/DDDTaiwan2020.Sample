using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace $safeprojectname$
{
    /// <summary>
    /// Infrstructure 相依性注入
    /// </summary>
    public static class DependencyInjections
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrstructureFeatures(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            /*
            services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(NorthwindContext).Assembly.FullName)));

            // 每次注入 IApplicationDbContext 時，即產生 NorthwindContext 實體
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<NorthwindContext>());
            */

            return services;
        }
    }
}
