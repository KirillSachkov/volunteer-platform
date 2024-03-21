using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VolunteerPlatform.Domain.Stores;
using VolunteerPlatform.Persistence;
using VolunteerPlatform.Persistence.Repositories;

namespace VolunteerPlatform.Application;

public static class DependencyRegistraction
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISqlConnectionFacroty, SqlConnectionFacroty>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)));
        });

        services.AddScoped<IOwnersRepository, OwnersRepository>();

        return services;
    }
}