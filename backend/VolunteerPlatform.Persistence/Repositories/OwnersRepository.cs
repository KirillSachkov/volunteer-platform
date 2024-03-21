using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using VolunteerPlatform.Domain.Common;
using VolunteerPlatform.Domain.Entities;
using VolunteerPlatform.Domain.Stores;

namespace VolunteerPlatform.Persistence.Repositories;

public class OwnersRepository : IOwnersRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly SqlConnectionFacroty _sqlConnectionFacroty;

    public OwnersRepository(ApplicationDbContext dbContext, SqlConnectionFacroty sqlConnectionFacroty)
    {
        _dbContext = dbContext;
        this._sqlConnectionFacroty = sqlConnectionFacroty;
    }

    public void Save(Owner owner)
    {
        _dbContext.Owners.Attach(owner);
    }

    public async Task<Result<Owner, Error>> GetById(Guid id, CancellationToken ct)
    {
        using var connection = _sqlConnectionFacroty.Create();

        var owner = await _dbContext.Owners
            .Include(o => o.Cats)
            .ThenInclude(c => c.Tags)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken: ct);

        if (owner is null)
            return Errors.General.NotFound(id);

        return owner;
    }
}