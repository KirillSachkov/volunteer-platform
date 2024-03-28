using Microsoft.Extensions.DependencyInjection;
using VolunteerPlatform.Application.Owners.Commands;

namespace VolunteerPlatform.IntegrationTests;

public class OwnerTests : BaseIntegrationTest
{
    public OwnerTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldAdd_NewOwnerToDb1()
    {
        //Arrange
        var command = new RegisterOwnerCommand(
            "Kirill",
            "+79661772402",
            "photo",
            "description",
            "login",
            "password");

        var handler = Scope.ServiceProvider.GetRequiredService<RegisterOwnerHandler>();

        //Act
        var ownerId = await handler.Handle(command);

        //Assert
        var owner = DbContext.Owners.FirstOrDefault(o => o.Id == ownerId.Value);

        Assert.NotNull(owner);
    }
}