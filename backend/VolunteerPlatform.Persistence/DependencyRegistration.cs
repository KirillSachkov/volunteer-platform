using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Domain.Stores;
using VolunteerPlatform.Persistence.Queries;
using VolunteerPlatform.Persistence.Repositories;

namespace VolunteerPlatform.Persistence;

public static class DependencyRegistraction
{
    public static IServiceCollection AddPersistense(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISqlConnectionFacroty, SqlConnectionFacroty>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOwnersRepository, OwnersRepository>();
        services.AddScoped<GetOwnersHandler>();

        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}