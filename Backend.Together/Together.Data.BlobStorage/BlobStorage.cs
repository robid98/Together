using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Together.Data.BlobStorage
{
    public class BlobStorage : IBlobStorage
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly ILogger<BlobStorage> _logger;

        public BlobStorage(BlobContainerClient blobContainerClient, ILogger<BlobStorage> logger)
        {
            _blobContainerClient = blobContainerClient;
            _logger = logger;
        }

        public ResultModel<string> GetBlobLink(string blobName)
        {
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(blobName);
                return new ResultModel<string> { Success = true, Result = blobClient.Uri.ToString() };
            }
            catch (Azure.RequestFailedException exception)
            {
                _logger.LogError(exception, $"A Blob Exception occurred while getting the blob link: {exception.Message}");

                if (exception.Status == 404)
                    return new ResultModel<string> { Success = false, Message = $"Blob with name  {blobName} not found in the BlobStorage" };

                return new ResultModel<string> { Success = false, Exception = true, Message = "Problem occured while getting the blob link!" };
            }
        }

        public async Task<ResultModel<string>> DeleteBlobAsync(string blobName)
        {
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(blobName);
                await blobClient.DeleteIfExistsAsync();

                return new ResultModel<string> { Success = true };
            }
            catch (Azure.RequestFailedException exception)
            {
                _logger.LogError(exception, $"A Blob Exception occurred while deleting the blob: {exception.Message}");

                if (exception.Status == 404)
                    return new ResultModel<string> { Success = false, Message = $"Blob with name  {blobName} not found in the BlobStorage" };

                return new ResultModel<string> { Success = false, Exception = true, Message = $"Problem occured while deleting the Blob with name {blobName}" };
            }
        }

        public async Task<ResultModel<string>> UploadContentBlobAsync(IFormFile formFile, string blobName)
        {
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(blobName);

                await blobClient.UploadAsync(formFile.OpenReadStream());

                return new ResultModel<string> { Success = true };
            }
            catch (Azure.RequestFailedException exception)
            {
                _logger.LogError(exception, $"A Blob Exception occurred while creating the blob: {exception.Message}");

                if (exception.Status == 409)
                    return new ResultModel<string> { Success = false, Message = ($"Blob with name {blobName} already exists in BlobStorage") };

                return new ResultModel<string> { Success = false, Exception = false, Message = "A problem occured while creating the Blob" };
            }
        }

        public async Task<ResultModel<byte[]>> GetBlobContent(string blobName)
        {
            try
            {
                var blobClient = _blobContainerClient.GetBlobClient(blobName);

                var downloadContent = await blobClient.DownloadAsync();

                using (MemoryStream ms = new MemoryStream())
                {
                    await downloadContent.Value.Content.CopyToAsync(ms);
                    return new ResultModel<byte[]> { Success = true, Result = ms.ToArray() };
                }
            }
            catch (Azure.RequestFailedException exception)
            {
                _logger.LogError(exception, $"A Blob Exception occurred while downloading the blob: {exception.Message}");

                if (exception.Status == 404)
                    return new ResultModel<byte[]> { Success = false, Message = ($"Blob with name {blobName} not found in BlobStorage") };

                return new ResultModel<byte[]> { Success = false, Exception = false, Message = "A problem occured while downloading the Blob" };
            }
        }
    }
}
