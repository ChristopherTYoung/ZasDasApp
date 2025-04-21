using Azure.Storage.Blobs;

namespace ZasAndDasWeb.Services
{
    public class BlobService(BlobContainerClient containerClient)
    {
        public async Task UploadImage(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var blobClient = containerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        public async Task<Stream> GetImage(string name)
        {
            var blobClient = containerClient.GetBlobClient(name);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }
    }
}
