namespace VolunteerPlatform.Application.Owners.Queries;

public record GetAllOwnersResponse(IEnumerable<OwnerDto> Owners);

public record OwnerDto(
    Guid Id,
    string Name,
    string PhoneNumber,
    string ProfilePhoto,
    string Description);
