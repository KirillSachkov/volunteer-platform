namespace VolunteerPlatform.Application.Owners.Queries;

public class OwnerReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string ProfilePhoto { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Login { get; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
}