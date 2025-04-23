using Square.Webhooks.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasAndDasWeb.Controllers;
using ZasAndDasWeb.Services;
using Shouldly;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure;
using Microsoft.AspNetCore.Http;
using NSubstitute;
namespace ZasAndDasMobile.Tests
{
    public class APITests
    {
        [Fact]
        public async Task Auth()
        {
            var auth = new AuthController(new MockAPIKeyValidationService());
            (await auth.Create(new ZasUndDas.Shared.Services.CreateRequest() { Name = "jeff", Email = "jeff@gmail.com", PassCode = "jefaf" })).ShouldBe("jeff@gmail.comtest");
            auth.Authenticate(new ZasUndDas.Shared.Services.AuthRequest() { Email = "jeff@gmail.com", PassCode = "jefaf" }).ShouldBe("jeff@gmail.comtest");
        }

        [Fact]
        public async Task CanUploadAndDownloadImages()
        {
            var testStream = new MemoryStream(Encoding.UTF8.GetBytes("fake image"));
            var file = new FormFile(testStream, 0, testStream.Length, "file", "test.png");

            // return object for the response for DownloadAsync
            var downloadResult = BlobsModelFactory.BlobDownloadInfo(
                content: testStream,
                contentType: "image/png"
            );

            // fakeResponse that DownloadAsync returns
            var fakeResponse = Substitute.For<Response<BlobDownloadInfo>>();
            fakeResponse.Value.Returns(downloadResult);

            var blobClient = Substitute.For<BlobClient>();
            blobClient.DownloadAsync().Returns(Task.FromResult(fakeResponse));

            var containerClient = Substitute.For<BlobContainerClient>();
            containerClient.GetBlobClient("test.png").Returns(blobClient);

            var blobService = new BlobService(containerClient);
            var blobController = new BlobController(blobService);

            var response = await blobController.UploadImageAsync(file);
            response.ShouldBe(Results.Ok());

            var image = await blobController.GetImageAsync("test.png");
            image.ContentType.ShouldBe("image/png");
        }
    }
}
