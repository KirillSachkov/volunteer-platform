using CSharpFunctionalExtensions;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Application.Requests;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.Entities;
using VolunteerPlatform.Domain.Stores;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Application.Services;

public class OwnersService
{
    private readonly IOwnersRepository _ownersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OwnersService(IOwnersRepository ownersRepository, IUnitOfWork unitOfWork)
    {
        _ownersRepository = ownersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> PublishCat(CreateCatRequest request, CancellationToken ct = default)
    {
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        var gender = Gender.Create(request.Gender).Value;

        var cat = Cat.Create(
            Guid.NewGuid(),
            request.Name,
            phoneNumber,
            request.Age,
            gender,
            request.Description,
            request.AnimalAttitude,
            request.PeopleAttitude,
            request.Vaccine,
            request.Color,
            request.Place,
            request.Health,
            []);

        if (cat.IsFailure)
            return cat.Error;

        var owner = await _ownersRepository.GetById(request.OwnerId, ct);
        if (owner.IsFailure)
            return owner.Error;

        var publishResult = owner.Value.PublishCat(cat.Value);
        if (publishResult.IsFailure)
            return new Error("public.cat", publishResult.Error);

        _ownersRepository.Save(owner.Value);
        await _unitOfWork.SaveChangesAsync(ct);

        return owner.Value.Id;
    }
}