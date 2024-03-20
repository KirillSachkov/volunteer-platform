using CSharpFunctionalExtensions;

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

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Login;
        yield return PasswordHash;
    }
}