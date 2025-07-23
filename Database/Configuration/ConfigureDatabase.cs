using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Context;
using Core.entities;
using Core.Interfaces;
using Database.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Configuration
{
    public static class ConfigureDatabase
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            var conn = configuration.GetConnectionString("MainDb")
        ?? throw new InvalidOperationException("Connection string 'MainDb' not found.");

            // попробовать AddPooledDbContext
            services.AddDbContext<PhotosContext>(options => options.UseSqlServer
                                                        (conn, x => x.MigrationsAssembly("Database.Migrations")));
            return services;

        }
    }
}
