using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection ApplyDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("MainDb")
        ?? throw new InvalidOperationException("Connection string 'MainDb' not found.");

            // попробовать AddPooledDbContext
            services.AddDbContext<PhotosContext>(options => options.UseSqlServer
                                                        (conn, x => x.MigrationsAssembly("Database.Migrations")));
            return services;

        }
    }
}
