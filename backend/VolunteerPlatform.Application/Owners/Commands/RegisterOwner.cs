using CSharpFunctionalExtensions;
using FluentValidation;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.Entities;
using VolunteerPlatform.Domain.Stores;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Application.Owners.Commands;

public record RegisterOwnerCommand(
        string Name,
        string PhoneNumber,
        string ProfilePhoto,
        string Description,
        string Login,
        string Password);

public class RegisterOwnerCommandValidator : AbstractValidator<PublishCatCommand>
{
    public RegisterOwnerCommandValidator()
    {
    }
}

public class RegisterOwnerHandler
{
    private readonly IOwnersRepository _ownersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterOwnerHandler(IOwnersRepository ownersRepository, IUnitOfWork unitOfWork)
    {
        _ownersRepository = ownersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(RegisterOwnerCommand command, CancellationToken ct = default)
    {
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var credentials = Credentials.Create(command.Login, command.Password).Value;

        var owner = Owner.Create(
            command.Name,
            phoneNumber,
            command.ProfilePhoto,
            command.Description,
            credentials);

        if (owner.IsFailure)
            return owner.Error;

        _ownersRepository.Save(owner.Value);
        await _unitOfWork.SaveChangesAsync(ct);

        return owner.Value.Id;
    }
}