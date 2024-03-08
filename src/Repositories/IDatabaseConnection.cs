using Npgsql;

namespace RinhAPI.Repositories;

public interface IDatabaseConnection
{
    NpgsqlConnection GetConnection();
}
