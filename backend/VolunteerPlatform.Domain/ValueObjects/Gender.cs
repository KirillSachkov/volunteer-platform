using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;

namespace VolunteerPlatform.Domain.ValueObjects;

public class Gender : ValueObject
{
    public static readonly Gender Male = new(nameof(Male));
    public static readonly Gender Female = new(nameof(Female));

    private static readonly Gender[] _all = [Male, Female];

    public string Value { get; }

    private Gender(string value)
    {
        Value = value;
    }

    public static Result<Gender, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired();

        var gender = input.Trim().ToUpper();

        if (_all.Any(g => g.Value == gender) == false)
            return Errors.General.ValueIsInvalid();

        return new Gender(gender);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}