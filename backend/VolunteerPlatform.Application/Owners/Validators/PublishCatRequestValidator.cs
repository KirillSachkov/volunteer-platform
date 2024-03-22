using FluentValidation;
using VolunteerPlatform.Application.Owners.Requests;
using VolunteerPlatform.Application.Utils;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Application.Owners.Validators;

public class PublishCatRequestValidator : AbstractValidator<PublishCatRequest>
{
    public PublishCatRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .MustBeValueObject(Gender.Create);

        RuleFor(x => new { x.Years, x.Months })
            .NotEmpty()
            .MustBeValueObject(x => Age.Create(x.Years, x.Months));
    }
}