using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ImageSearcher.Web.Api.Tests
{
    public class ImagesTests : ApiTestBase
    {
        [Theory]
        [InlineData("tags=cat&tags=")]
        [InlineData("tags=cat&longitude=-181")]
        [InlineData("tags=cat&longitude=181")]
        [InlineData("tags=cat&longitude=180&latitude=-91")]
        [InlineData("tags=cat&longitude=180&latitude=91")]
        public async Task Images_Search_ValidatesImageFilter(string query)
        {
            var response = await Client.GetAsync($"/api/images?{query}");

            response.AssertStatusCode(HttpStatusCode.BadRequest);
        }
    }
}
