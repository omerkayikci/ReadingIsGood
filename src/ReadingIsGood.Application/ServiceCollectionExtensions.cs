using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReadingIsGood.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            Assembly assm = Assembly.GetExecutingAssembly();
            static bool Expression(Type type) => typeof(IApplicationService).IsAssignableFrom(type);

            foreach (Type v in assm.GetTypes().Where(type => !type.IsInterface && Expression(type) && type.GetInterfaces().Any(Expression)))
            {
                foreach (Type i in v.GetInterfaces().Where(Expression))
                {
                    services.AddScoped(i, v);
                }
            }
        }
    }
}
