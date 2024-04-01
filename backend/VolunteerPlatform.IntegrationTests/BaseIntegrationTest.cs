using Microsoft.Extensions.DependencyInjection;
using VolunteerPlatform.Infrastructure;

namespace VolunteerPlatform.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
{
    private readonly IntegrationTestWebAppFactory _factory;

    protected readonly IServiceScope Scope;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        Scope = factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeRespawner();
    }

    public async Task DisposeAsync()
    {
        await _factory.ResetDatabaseAsync();
        await DbContext.DisposeAsync();
        Scope.Dispose();
    }
}