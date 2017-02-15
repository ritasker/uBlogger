namespace uBlogger.Infrastructure.Database
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