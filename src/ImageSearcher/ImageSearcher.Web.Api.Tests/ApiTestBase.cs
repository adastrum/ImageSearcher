using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;

namespace ImageSearcher.Web.Api.Tests
{
    public abstract class ApiTestBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        protected ApiTestBase()
        {
            var testPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(testPath, "../../../../ImageSearcher.Web.Api"));
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development");

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }
    }
}
