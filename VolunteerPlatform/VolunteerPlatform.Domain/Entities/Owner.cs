using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Domain.Entities;

public class Owner : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 200;
    public const int MAX_DESCRIPTION_LENGTH = 3000;

    private readonly List<Cat> _cats = [];

    private Owner(
        Guid id,
        string name,
        PhoneNumber phoneNumber,
        string profilePhoto,
        string description,
        Credentials credentials) : base(id)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        ProfilePhoto = profilePhoto;
        Description = description;
        Credentials = credentials;
    }

    public string Name { get; }
    public PhoneNumber PhoneNumber { get; }
    public string ProfilePhoto { get; }
    public string Description { get; }
    public Credentials Credentials { get; }
    
    public IReadOnlyList<Cat> Cats => _cats;

    public Result PublishCat(Cat cat)
    {
        _cats.Add(cat);
        return Result.Success();
    }

    private static Result<Owner, Error> Create(
        Guid id,
        string name,
        PhoneNumber phoneNumber,
        string profilePhoto,
        string description,
        Credentials credentials)
    {
        if (id == Guid.Empty)
            Errors.General.ValueIsInvalid();

        if (string.IsNullOrWhiteSpace(name) == false || name.Length > MAX_NAME_LENGTH)
            Errors.General.InvalidLength("name");

        if (description.Length > MAX_DESCRIPTION_LENGTH)
            Errors.General.InvalidLength("description");

        return new Owner(id, name, phoneNumber, profilePhoto, description, credentials);
    }
}