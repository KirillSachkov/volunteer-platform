namespace VolunteerPlatform.Domain.Dto;

public class OwnerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ProfilePhoto { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Login { get; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
