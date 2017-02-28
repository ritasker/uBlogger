using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure
{
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration(string connectionString)
        {
            Database = new DatabaseConfiguration(connectionString);
        }

        public DatabaseConfiguration Database { get; set; }
    }
}