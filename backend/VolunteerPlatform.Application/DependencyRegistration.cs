using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using VolunteerPlatform.Application.Abstractions.Messaging;
using VolunteerPlatform.Application.Decorators;
using VolunteerPlatform.Application.Owners.Commands;
using VolunteerPlatform.Application.Utils;

namespace VolunteerPlatform.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);
        services.AddFluentValidationAutoValidation();

        services.AddScoped<PublishCatHandler>();
        // services.AddScoped<RegisterOwnerHandler>();

        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyRegistration).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}