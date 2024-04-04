namespace VolunteerPlatform.Application.Services;

public interface ICacheService
{
    Task<T> GetOrCreate<T>(
        string cacheKey,
        Func<Task<T>> factory,
        CancellationToken ct) where T : class;
}