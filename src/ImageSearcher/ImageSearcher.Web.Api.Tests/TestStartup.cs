using Microsoft.Extensions.Configuration;

namespace ImageSearcher.Web.Api.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
