using Npgsql;

namespace RinhAPI.Repositories;

public class DatabaseConnection: IDatabaseConnection
{
    private readonly NpgsqlDataSource _dataSource;
    public DatabaseConnection()
    {
        string conectionString = $"Host=db;Port=5432;Database=rinhadb;Username=postgres;Password=postgres";
        _dataSource = new NpgsqlSlimDataSourceBuilder(conectionString).Build();
    }

    public NpgsqlConnection GetConnection(){
        var connection = _dataSource.OpenConnection();
        return connection;
    }
}
