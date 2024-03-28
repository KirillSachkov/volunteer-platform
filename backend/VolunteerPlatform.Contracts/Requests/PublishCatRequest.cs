namespace VolunteerPlatform.Contracts.Requests;

public record PublishCatRequest(
    string Name,
    string PhoneNumber,
    int Years,
    int Months,
    string Gender,
    string Description,
    string AnimalAttitude,
    string PeopleAttitude,
    bool Vaccine,
    string Color,
    string Place,
    string Health,
    IEnumerable<string> Tags);