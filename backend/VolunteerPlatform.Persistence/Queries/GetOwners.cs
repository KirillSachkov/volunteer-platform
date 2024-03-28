using Dapper;
using Npgsql;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Application.Owners.Queries;

namespace VolunteerPlatform.Persistence.Queries;

public record GetOwnersQuery(List<OwnerReadModel> Owners);

public class GetOwnersHandler
{
    private readonly ISqlConnectionFacroty _sqlConnectionFacroty;

    public GetOwnersHandler(ISqlConnectionFacroty sqlConnectionFacroty)
    {
        _sqlConnectionFacroty = sqlConnectionFacroty;
    }

    public async Task<GetOwnersQuery> Handle(CancellationToken ct = default)
    {
        using var connection = _sqlConnectionFacroty.Create();

        var owners = await connection.QueryAsync<OwnerReadModel>(
            "SELECT * FROM Owners"
        );

        return new(owners.ToList());
    }
}