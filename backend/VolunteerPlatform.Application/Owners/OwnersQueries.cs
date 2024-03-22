using Dapper;
using VolunteerPlatform.Application.Abstractions;
using VolunteerPlatform.Domain.Dto;

namespace VolunteerPlatform.Application.Owners;

public class OwnersQueries
{
    // private readonly ISqlConnectionFacroty _sqlConnectionFacroty;
    //
    // public OwnersQueries(ISqlConnectionFacroty sqlConnectionFacroty)
    // {
    //     _sqlConnectionFacroty = sqlConnectionFacroty;
    // }
    //
    // public async Task<OwnerDto> GetById(Guid id, CancellationToken ct = default)
    // {
    //     using var connection = _sqlConnectionFacroty.Create();
    //
    //     var ownerResponse = await connection.QueryFirstOrDefaultAsync<OwnerDto>(
    //         """"
    //         SELECT
    //         """"
    //     );
    //
    //     return new OwnerDto();
    // }
}