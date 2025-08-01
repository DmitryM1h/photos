using System.Reflection;
using Application.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class Configure
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
         => services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMappersFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(t =>
                {
                    t.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    
                });

           //AddTransient.TypeOf();


        
    }
}
