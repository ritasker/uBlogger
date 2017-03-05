using System.Threading.Tasks;
using Newtonsoft.Json;

namespace uBlogger.Infrastructure.MessageBus
{
    using Microsoft.Azure.ServiceBus;

    public class ServiceBusClient
    {
        private readonly ServiceBusConfiguration configuration;

        public ServiceBusClient(ServiceBusConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Send<T>(T message)
        {
            var queueClient = CreateClient(typeof(T).Name);
            await queueClient.SendAsync(new BrokeredMessage(JsonConvert.SerializeObject(message)));
            queueClient.CloseAsync().GetAwaiter();
        }

        private QueueClient CreateClient(string entityName)
        {
            var connectionStringBuilder = new ServiceBusConnectionStringBuilder(configuration.ConnectionString)
            {
                EntityPath = entityName
            };
            return QueueClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
    }
}