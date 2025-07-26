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
