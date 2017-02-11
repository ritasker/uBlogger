using System.Data;
using Npgsql;

namespace uBlogger.Infrastructure.Database
{
    public class PostgresConnectionProvider : IDbConnectionProvider
    {
        private readonly DatabaseConfiguration _config;

        public PostgresConnectionProvider(DatabaseConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_config.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}