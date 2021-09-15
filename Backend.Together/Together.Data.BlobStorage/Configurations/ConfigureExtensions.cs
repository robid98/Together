using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Together.Data.BlobStorage.Configurations
{
    public static class ConfigureExtensions
    {
        public static BlobServiceClient CreateBlobServiceClient(IConfiguration configuration)
        {
            return new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorageConnectionString"));
        }

        public static BlobContainerClient CreateBlobContainerClient(IConfiguration configuration)
        {
            var blobServiceClient = CreateBlobServiceClient(configuration);

            var blobContainerClient = blobServiceClient.GetBlobContainerClient("userprofilepictures");

            blobContainerClient.CreateIfNotExists();

            return blobContainerClient;
        }

        public static void AddBlobService(this IServiceCollection services, IConfiguration configuration)
        {
            var blobContainerClient = CreateBlobContainerClient(configuration);

            services.AddSingleton<BlobServiceClient>(x => CreateBlobServiceClient(configuration));

            services.AddScoped<IBlobStorage>(serviceProvider =>
            {
                var blobLogger = serviceProvider.GetRequiredService<ILogger<BlobStorage>>();

                return new BlobStorage(blobContainerClient, blobLogger);
            });
        }
    }
}
