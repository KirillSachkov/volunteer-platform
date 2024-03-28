using CSharpFunctionalExtensions;
using FluentValidation;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Application.Utils;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.Entities;
using VolunteerPlatform.Domain.Stores;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Application.Owners.Commands;

public record PublishCatCommand(
    Guid OwnerId,
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

public class PublishCatRequestValidator : AbstractValidator<PublishCatCommand>
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

public class PublishCatHandler
{
    private readonly IOwnersRepository _ownersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PublishCatHandler(IOwnersRepository ownersRepository, IUnitOfWork unitOfWork)
    {
        _ownersRepository = ownersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(PublishCatCommand command, CancellationToken ct = default)
    {
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var gender = Gender.Create(command.Gender).Value;
        var age = Age.Create(command.Years, command.Months).Value;

        var cat = Cat.Create(
            Guid.NewGuid(),
            command.Name,
            phoneNumber,
            age,
            gender,
            command.Description,
            command.AnimalAttitude,
            command.PeopleAttitude,
            command.Vaccine,
            command.Color,
            command.Place,
            command.Health,
            []);

        if (cat.IsFailure)
            return cat.Error;

        var owner = await _ownersRepository.GetById(command.OwnerId, ct);
        if (owner.IsFailure)
            return owner.Error;

        owner.Value.PublishCat(cat.Value);
        _ownersRepository.Save(owner.Value);
        await _unitOfWork.SaveChangesAsync(ct);

        return owner.Value.Id;
    }
}