using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using VolunteerPlatform.Application.Abstractions;

namespace VolunteerPlatform.Persistence;

public class SqlConnectionFacroty : ISqlConnectionFacroty
{
    private readonly string _connectionString;

    public SqlConnectionFacroty(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext))
            ?? throw new ApplicationException("Connection stirng is missing");
    }

    public IDbConnection Create()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
