using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using ZasAndDasWeb.Services;

namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController(BlobService blobService)
    {
        [HttpGet("images/{filename}")]
        public async Task<FileStreamResult> GetImageAsync(string filename)
        {
            var stream = await blobService.GetImage(filename);
            return new FileStreamResult(stream, "image/png");
        }

        [HttpPost("images/upload")]
        public async Task<IResult> UploadImageAsync(IFormFile image)
        {
            await blobService.UploadImage(image);
            return Results.Ok();
        }
    }
}
