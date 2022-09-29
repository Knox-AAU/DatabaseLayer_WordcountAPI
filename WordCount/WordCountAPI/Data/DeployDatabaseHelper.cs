using System.Data;
using Npgsql;

namespace WordCount.Data;

public class DeployDatabaseHelper
{
    public void Deploy()
    {
        using IDbConnection db = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres");
    }
}