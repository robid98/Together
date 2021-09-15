using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Together.Data.BlobStorage
{
    public interface IBlobStorage
    {
        Task<ResultModel<string>> UploadContentBlobAsync(IFormFile formFile, string blobName);

        Task<ResultModel<string>> DeleteBlobAsync(string blobName);

        ResultModel<string> GetBlobLink(string blobName);

        public Task<ResultModel<byte[]>> GetBlobContent(string blobName);
    }
}
