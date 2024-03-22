using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Domain.Entities;

public class Owner : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 200;
    public const int MAX_DESCRIPTION_LENGTH = 3000;

    private readonly List<Cat> _cats = [];

    private Owner()
    {
    }

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

    public string Name { get; } = string.Empty;
    public PhoneNumber PhoneNumber { get; } = null!;
    public string ProfilePhoto { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public Credentials Credentials { get; } = null!;

    public IReadOnlyList<Cat> Cats => _cats;

    public void PublishCat(Cat cat)
    {
        _cats.Add(cat);
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