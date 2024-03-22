using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;

namespace VolunteerPlatform.Domain.ValueObjects;

public class Age : ValueObject
{
    public int Years { get; }

    public int Months { get; }

    private Age(int years, int months)
    {
        Years = years;
        Months = months;
    }

    public static Result<Age, Error> Create(int years, int months)
    {
        if (years <= 0)
            return Errors.General.InvalidLength("years");

        if (months is <= 0 or > 12)
            return Errors.General.InvalidLength("months");

        return new Age(years, months);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Years;
        yield return Months;
    }
}