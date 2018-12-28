using System.IO;
using System.Net.Http;
using ImageSearcher.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Moq;

namespace ImageSearcher.Web.Api.Tests
{
    public abstract class ApiTestBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected Mock<IHttpHandler> HttpHandlerMock;
        protected Mock<IImageService> ImageServiceMock;
        protected Mock<ICachingService> CachingServiceMock;

        protected ApiTestBase()
        {
            var testPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(testPath, "../../../../ImageSearcher.Web.Api"));
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    HttpHandlerMock = new Mock<IHttpHandler>();
                    services.AddSingleton(HttpHandlerMock.Object);
                    ImageServiceMock = new Mock<IImageService>();
                    services.AddSingleton(ImageServiceMock.Object);
                    CachingServiceMock = new Mock<ICachingService>();
                    services.AddSingleton(CachingServiceMock.Object);
                })
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development");

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }
    }
}
