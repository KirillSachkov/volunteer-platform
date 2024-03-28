using Dapper;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Application.Owners.Queries;

namespace VolunteerPlatform.Persistence.Queries;

public class GetOwnersHandler
{
    private readonly ISqlConnectionFacroty _sqlConnectionFacroty;

    public GetOwnersHandler(ISqlConnectionFacroty sqlConnectionFacroty)
    {
        _sqlConnectionFacroty = sqlConnectionFacroty;
    }

    public async Task<GetAllOwnersResponse> Handle()
    {
        using var connection = _sqlConnectionFacroty.Create();

        const string sql =
            """
            SELECT id, name, phone_number, profile_photo, o.description
            FROM Owners o
            """;

        var owners = await connection.QueryAsync<OwnerDto>(sql);

        return new GetAllOwnersResponse(owners);
    }
}

public class GetOwnerByIdHandler
{
    private readonly ISqlConnectionFacroty _sqlConnectionFacroty;

    public GetOwnerByIdHandler(ISqlConnectionFacroty sqlConnectionFacroty)
    {
        _sqlConnectionFacroty = sqlConnectionFacroty;
    }

    public async Task<GetAllOwnersResponse> Handle()
    {
        using var connection = _sqlConnectionFacroty.Create();

        const string sql =
            """
            SELECT id, name, phone_number, profile_photo, o.description
            FROM Owners o
            """;

        var owners = await connection.QueryAsync<OwnerDto>(sql);

        return new GetAllOwnersResponse(owners);
    }
}