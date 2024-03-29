﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using VolunteerPlatform.Application.Owners.Commands;

namespace VolunteerPlatform.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);
        services.AddFluentValidationAutoValidation();

        services.AddScoped<PublishCatHandler>();
        services.AddScoped<RegisterOwnerHandler>();
        
        return services;
    }
}