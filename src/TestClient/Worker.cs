using TestClient.Services;

namespace TestClient
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly ServiceBusQueueService serviceBusQueueService;
        private readonly StorageService storageService;

        public Worker(ILogger<Worker> logger, 
            ServiceBusQueueService serviceBusQueueService,
            StorageService storageService)
        {
            this.logger = logger;
            this.serviceBusQueueService = serviceBusQueueService;
            this.storageService = storageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var filename = await storageService.CreateFile($"new file content created at {DateTime.UtcNow}.");
            logger.LogInformation("File {filename} is created.", filename);

            await serviceBusQueueService.CreateEvent(filename);
            logger.LogInformation("Event is created.");
        }
    }
}