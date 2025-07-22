using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //return services.AddScoped<IValidator<UserDto>,UserValidator>();
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}
