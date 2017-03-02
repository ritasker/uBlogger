namespace uBlogger.Infrastructure.DataAccess
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}