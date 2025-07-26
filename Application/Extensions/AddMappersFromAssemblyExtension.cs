using System.Reflection;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMappersFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var mapperTypes = assembly.GetTypes()
                .Where(t => t.IsClass &&
                           !t.IsAbstract &&
                           t.GetInterfaces()
                            .Any(i => i.IsGenericType &&
                                     i.GetGenericTypeDefinition() == typeof(IMapper<,>)));

            foreach (var mapperType in mapperTypes)
            {
                var mapperInterface = mapperType.GetInterfaces()
                    .First(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IMapper<,>));

                services.AddScoped(mapperInterface, mapperType);
            }

            return services;
        }
    }
}