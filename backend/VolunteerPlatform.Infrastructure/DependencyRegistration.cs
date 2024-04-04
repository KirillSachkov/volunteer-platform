using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Application.Services;
using VolunteerPlatform.Domain.Stores;
using VolunteerPlatform.Infrastructure.Queries;
using VolunteerPlatform.Infrastructure.Repositories;
using VolunteerPlatform.Infrastructure.Services;

namespace VolunteerPlatform.Infrastructure;

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

        services.AddMinio(options =>
        {
            options.Endpoint = "127.0.0.1:9000";
            options.AccessKey = "minio";
            options.SecretKey = "minio123";
        });

        services.AddMemoryCache();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOwnersRepository, OwnersRepository>();

        services.AddScoped<GetOwnersHandler>();

        services.AddScoped<IMinioService, MinioService>();
        services.AddScoped<ICacheService, CacheService>();

        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}