using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Application.Mediator.Processors;
using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.Services.Abstractions;
using System;
using System.Linq;
using System.Reflection;

namespace ReadingIsGood.Application.Mediator
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            Assembly appAssm = Assembly.GetAssembly(typeof(CommandQueryMediator))!;
            Assembly coreAssm = Assembly.GetAssembly(typeof(IApplicationService))!;

            services.AddMediatr(appAssm);
            services.AddHandlers(appAssm);
            services.AddValidators(coreAssm);

            services.AddScoped<ICommandSender, CommandQueryMediator>();
            services.AddScoped<IQueryProcessor, CommandQueryMediator>();

            return services;
        }

        private static void AddMediatr(this IServiceCollection services, Assembly assm)
        {
            services.AddMediatR(assm)
                    .AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationRequestPreProcessor<>));
        }

        private static void AddHandlers(this IServiceCollection services, Assembly assembly)
        {
            static bool Expression(Type type)
            {
                return type.Is(typeof(ICommandHandler<,>))
                  || type.Is(typeof(IQueryHandler<,>));
            }

            foreach (Type h in assembly.GetTypes().Where(type => type.GetInterfaces().Any(Expression)))
            {
                foreach (Type hi in h.GetInterfaces().Where(Expression))
                {
                    services.AddScoped(hi, h);
                }
            }
        }

        private static void AddValidators(this IServiceCollection services, Assembly assembly)
        {
            static bool Expression(Type type) => type.Is(typeof(IValidator<>));

            foreach (Type v in assembly.GetTypes().Where(type => type.GetInterfaces().Any(Expression)))
            {
                foreach (Type i in v.GetInterfaces().Where(Expression))
                {
                    services.AddScoped(i, v);
                }
            }
        }

        private static bool Is(this Type type, Type typeCompare)
        {
            return type.IsGenericType && (type.Name.Equals(typeCompare.Name, StringComparison.InvariantCulture) || type.GetGenericTypeDefinition() == typeCompare);
        }
    }
}
