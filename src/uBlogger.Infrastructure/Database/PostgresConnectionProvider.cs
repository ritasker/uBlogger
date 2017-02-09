using System.Data;
using Npgsql;

namespace uBlogger.Infrastructure.Database
{
    public class PostgresConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetConnection()
        {
            var conn = new NpgsqlConnection();
            conn.Open();
            return conn;
        }
    }
}