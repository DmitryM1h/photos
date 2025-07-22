using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.mappers;
using Core.entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
         => services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddScoped<IMapper<UserDto,User>,UserMapper>();


        
    }
}
