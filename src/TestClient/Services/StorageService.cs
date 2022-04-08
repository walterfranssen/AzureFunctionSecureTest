using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace TestClient.Services
{
    internal class StorageService
    {
        private readonly StorageServiceOptions options;

        public StorageService(IOptions<StorageServiceOptions> options)
        {
            this.options = options.Value;
        }
        public async Task<string> CreateFile(string fileContent)
        { 
            BlobContainerClient blobContainerClient = new(options.ConnectionString, options.ContainerName);

            await blobContainerClient.CreateIfNotExistsAsync();

            var fileName = $"{Guid.NewGuid()}.txt";
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(BinaryData.FromString(fileContent));

            return fileName;
        }
    }
}
