using FluentValidation;

namespace VolunteerPlatform.Application.Owners.Requests;

public record PublishCatRequest(
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

public class PublishCatRequestValidator : AbstractValidator<PublishCatRequest>
{
    public PublishCatRequestValidator()
    {

    }
}