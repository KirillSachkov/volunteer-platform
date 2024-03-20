using CSharpFunctionalExtensions;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Domain.Stores;

public interface IOwnersRepository
{
    void Save(Owner owner);
    Task<Result<Owner, Error>> GetById(Guid id, CancellationToken ct = default);
}