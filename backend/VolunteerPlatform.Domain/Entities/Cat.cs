﻿using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.ValueObjects;

namespace VolunteerPlatform.Domain.Entities;

public class Cat : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 200;
    public const int MAX_TITLE_LENGTH = 200;
    public const int MAX_DESCRIPTION_LENGTH = 3000;

    private readonly List<Tag> _tags = [];

    private Cat() 
    {
    }

    private Cat(
        Guid id,
        string name,
        PhoneNumber phoneNumber,
        Age age,
        Gender gender,
        string description,
        string? animalAttitude,
        string? peopleAttitude,
        bool? vaccine,
        string? color,
        string? place,
        string? health,
        IEnumerable<Tag> tags) : base(id)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Age = age;
        Gender = gender;
        Description = description;
        AnimalAttitude = animalAttitude;
        PeopleAttitude = peopleAttitude;
        Vaccine = vaccine;
        Color = color;
        Place = place;
        Health = health;
        _tags = tags.ToList();
    }

    public PhoneNumber PhoneNumber { get; } = null!;
    public Gender Gender { get; } = null!;
    public Age Age { get; } = null!;
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public string? AnimalAttitude { get; }
    public string? PeopleAttitude { get; }
    public bool? Vaccine { get; }
    public string? Color { get; }
    public string? Place { get; }
    public string? Health { get; }

    public IReadOnlyList<Tag> Tags => _tags;

    public static Result<Cat, Error> Create(
        Guid id,
        string name,
        PhoneNumber phoneNumber,
        Age age,
        Gender gender,
        string description,
        string? animalAttitude,
        string? peopleAttitude,
        bool? vaccine,
        string? color,
        string? place,
        string? health,
        IEnumerable<Tag> tags)
    {
        if (id == Guid.Empty)
            return Errors.General.ValueIsRequired();

        if (string.IsNullOrWhiteSpace(name) == false || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidLength("name");

        if (description.Length > MAX_DESCRIPTION_LENGTH)
            return Errors.General.InvalidLength("description");

        if (animalAttitude?.Length > MAX_TITLE_LENGTH)
            return Errors.General.InvalidLength("animal attitude");

        if (peopleAttitude?.Length > MAX_TITLE_LENGTH)
            return Errors.General.InvalidLength("people attitude");

        if (color?.Length > MAX_TITLE_LENGTH)
            return Errors.General.InvalidLength("color");

        if (place?.Length > MAX_TITLE_LENGTH)
            return Errors.General.InvalidLength("place");

        if (health?.Length > MAX_TITLE_LENGTH)
            return Errors.General.InvalidLength("health");

        return new Cat(
            id,
            name,
            phoneNumber,
            age,
            gender,
            description,
            animalAttitude,
            peopleAttitude,
            vaccine,
            color,
            place,
            health,
            tags);
    }
}