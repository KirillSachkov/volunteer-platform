using System.Data;

namespace VolunteerPlatform.Application.Abstractions;

public interface ISqlConnectionFacroty
{
    IDbConnection Create();
}