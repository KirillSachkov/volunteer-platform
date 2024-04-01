using CSharpFunctionalExtensions;

namespace VolunteerPlatform.Domain.Entities;

public class MainPhoto : ValueObject
{
    public const string BUCKET_NAME = "images";

    private MainPhoto(string path, string contentType)
    {
        Path = path;
        ContentType = contentType;
    }

    public string Path { get; }

    public string ContentType { get; }

    public static Result<MainPhoto> Create(string extension, string contentType)
    {
        var id = Guid.NewGuid();
        var path = $"{id}{extension}";

        return new MainPhoto(path, contentType);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Path;
    }
}