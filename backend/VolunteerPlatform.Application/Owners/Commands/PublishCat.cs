using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Application.Abstractions.Messaging;
using VolunteerPlatform.Application.Services;
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
    DateTime BirthDate,
    string Gender,
    string Description,
    string AnimalAttitude,
    string PeopleAttitude,
    bool Vaccine,
    string Color,
    string Place,
    string Health,
    IFormFile MainPhoto,
    IEnumerable<string> Tags) : ICommand;

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
    }
}

public class PublishCatHandler : ICommandHandler<PublishCatCommand>
{
    private readonly IOwnersRepository _ownersRepository;
    private readonly IMinioService _minioService;
    private readonly IUnitOfWork _unitOfWork;

    public PublishCatHandler(
        IOwnersRepository ownersRepository,
        IMinioService minioService,
        IUnitOfWork unitOfWork)
    {
        _ownersRepository = ownersRepository;
        _minioService = minioService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(PublishCatCommand command, CancellationToken ct = default)
    {
        var contentType = command.MainPhoto.ContentType;
        var extension = Path.GetExtension(command.MainPhoto.FileName);
        var mainPhoto = MainPhoto.Create(extension, contentType).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var gender = Gender.Create(command.Gender).Value;

        var cat = Cat.Create(
            command.Name,
            phoneNumber,
            command.BirthDate,
            gender,
            command.Description,
            command.AnimalAttitude,
            command.PeopleAttitude,
            command.Vaccine,
            command.Color,
            command.Place,
            command.Health,
            mainPhoto,
            []);

        if (cat.IsFailure)
            return Result.Failure("");

        var owner = await _ownersRepository.GetById(command.OwnerId, ct);
        if (owner.IsFailure)
            return Result.Failure("");

        owner.Value.PublishCat(cat.Value);
        _ownersRepository.Save(owner.Value);
        await _unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}