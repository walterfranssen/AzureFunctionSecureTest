using TestClient;
using TestClient.Services;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddUserSecrets<Program>()
                            .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.Configure<StorageServiceOptions>(configuration.GetSection(nameof(StorageServiceOptions)));
        services.Configure<ServiceBusQueueOptions>(configuration.GetSection(nameof(ServiceBusQueueOptions)));

        services.AddSingleton<ServiceBusQueueService>();
        services.AddSingleton<StorageService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
