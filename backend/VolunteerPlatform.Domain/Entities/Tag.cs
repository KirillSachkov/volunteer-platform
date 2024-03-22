using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;

namespace VolunteerPlatform.Domain.Entities;

public class Tag : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 200;

    private Tag()
    {
    }

    private Tag(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; } = string.Empty;

    public static Result<Tag, IReadOnlyList<Error>> Create(Guid id, string name)
    {
        if (id == Guid.Empty)
            Errors.General.ValueIsInvalid();

        if (string.IsNullOrWhiteSpace(name) == false || name.Length > MAX_NAME_LENGTH)
            Errors.General.InvalidLength("name");

        return new Tag(id, name);
    }
}