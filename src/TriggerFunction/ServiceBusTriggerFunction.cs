using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TriggerFunction
{
    public class ServiceBusTriggerFunction
    {
        [FunctionName("ServiceBusQueueTrigger")]
        public async Task Run([ServiceBusTrigger("filecreatedqueue", Connection = "ServiceBusQueueConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            await ReadFileFromStorageAccount(fileName: myQueueItem, log);
        }

        private async Task ReadFileFromStorageAccount(string fileName, ILogger log)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureStorageAccountConnectionString");
            string container = Environment.GetEnvironmentVariable("AzureStorageAccountContainerName");
   
            var client = new Azure.Storage.Blobs.BlobContainerClient(connectionString, container);
            var blobClient = client.GetBlobClient(fileName);

            var result = await blobClient.DownloadContentAsync();
            var content = result.Value.Content.ToString();

            log.LogInformation("Filecontent is: {fileContent}", content);
        }
    }
}
