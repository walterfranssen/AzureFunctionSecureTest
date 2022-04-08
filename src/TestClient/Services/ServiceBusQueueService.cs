using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace TestClient.Services
{
    internal class ServiceBusQueueService
    {
        private readonly ServiceBusQueueOptions options;

        public ServiceBusQueueService(IOptions<ServiceBusQueueOptions> options)
        {
            this.options = options.Value;
        }
        public async Task CreateEvent(string fileName, CancellationToken cancellationToken = default)
        { 
            var client = new ServiceBusClient(options.ConnectionString);
            var queue = client.CreateSender(options.QueueName);

            ServiceBusMessage message = new() { Body = BinaryData.FromString(fileName) };
            await queue.SendMessageAsync(message, cancellationToken);
        }
    }
}
