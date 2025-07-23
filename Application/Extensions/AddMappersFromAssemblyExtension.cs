using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class AddMappersFromAssemblyExtension
    {
        public static IServiceCollection AddMappersFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            //var validators = assembly.DefinedTypes.Where(t =>
            //{
            //    //return t.ImplementedInterfaces.Select(u => u.Name).Where(t => t =="IMapper");

            //});

            return services;
        }

    }
}
