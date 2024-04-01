using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Application.Services;

public interface IMinioService
{
    Task<Result> UploadImage(Stream stream, MainPhoto mainPhoto, CancellationToken ct);
    Task<Stream> GetImage(string fileName, CancellationToken ct);
}