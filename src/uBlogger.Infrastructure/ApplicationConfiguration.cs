using uBlogger.Infrastructure.Database;
using uBlogger.Infrastructure.Email;

namespace uBlogger.Infrastructure
{
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration(EmailConfiguation emailConfig, string connectionString)
        {
            Email = emailConfig;
            Database = new DatabaseConfiguration(connectionString);
        }

        public DatabaseConfiguration Database { get; set; }
        public EmailConfiguation Email { get; set; }
    }
}