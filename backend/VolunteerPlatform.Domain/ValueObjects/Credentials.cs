using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;

namespace VolunteerPlatform.Domain.ValueObjects;

public class Credentials : ValueObject
{
    private Credentials(string login, string passwordHash)
    {
        Login = login;
        PasswordHash = passwordHash;
    }

    public string Login { get; }

    public string PasswordHash { get; }

    public static Result<Credentials, Error> Create(string login, string password)
    {
        return new Credentials(login, password);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Login;
        yield return PasswordHash;
    }
}