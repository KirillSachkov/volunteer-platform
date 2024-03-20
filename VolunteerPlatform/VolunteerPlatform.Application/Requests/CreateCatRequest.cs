namespace VolunteerPlatform.Application.Requests;

public record CreateCatRequest(
    Guid OwnerId,
    string Name,
    string PhoneNumber,
    DateTime Age,
    string Gender,
    string Description,
    string AnimalAttitude,
    string PeopleAttitude,
    bool Vaccine,
    string Color,
    string Place,
    string Health,
    IEnumerable<string> Tags);