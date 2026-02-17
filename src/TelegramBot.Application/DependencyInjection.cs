using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TelegramBot.Application.Common.Interfaces;

namespace TelegramBot.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // add handlers
            services.Scan(scan => scan
                .FromAssemblyOf<ITelegramUpdateHandler>()
                .AddClasses(classes => classes.AssignableTo<ITelegramUpdateHandler>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
