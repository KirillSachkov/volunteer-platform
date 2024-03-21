using System.Data;

namespace VolunteerPlatform.Persistence;

public interface ISqlConnectionFacroty
{
    IDbConnection Create();
}