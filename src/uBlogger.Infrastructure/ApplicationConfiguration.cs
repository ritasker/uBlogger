using uBlogger.Infrastructure.DataAccess;
using uBlogger.Infrastructure.MessageBus;

namespace uBlogger.Infrastructure
{
    public class ApplicationConfiguration
    {
        public DatabaseConfiguration Database { get; set; }
        public ServiceBusConfiguration ServiceBus { get; set; }
    }
}