namespace VolunteerPlatform.Application.Owners.Queries;

public class OwnerReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string PhoneNumber { get; init; }
    public string ProfilePhoto { get; init; }
    public string Description { get; init; }
    public string Login { get; init; }
    public string PasswordHash { get; init; }
}