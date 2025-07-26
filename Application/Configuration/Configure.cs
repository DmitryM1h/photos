using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Extensions;
using Application.mappers;
using Core.Dtos;
using Core.entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class Configure
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
         => services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                //.AddMappersFromAssembly(Assembly.GetExecutingAssembly())
                .AddScoped<IMapper<UserDto, User>, UserMapper>()
                .AddScoped<IMapper<PhotoDto, Photo>, PhotoMapper>()
                .AddMediatR(t =>
                {
                    t.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    // че то тут
                });

           //AddTransient.TypeOf();


        
    }
}
