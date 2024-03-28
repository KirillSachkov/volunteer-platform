using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VolunteerPlatform.Persistence;

namespace VolunteerPlatform.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    protected readonly IServiceScope Scope;
    protected readonly ApplicationDbContext DbContext;

    public BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Scope = factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Scope.Dispose();
        DbContext.Dispose();
    }
}
