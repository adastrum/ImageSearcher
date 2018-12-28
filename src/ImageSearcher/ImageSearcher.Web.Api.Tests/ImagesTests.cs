using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ImageSearcher.Core;
using ImageSearcher.Core.DTO;
using Moq;
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
        [InlineData("tags=cat&longitude=180&latitude=90&pagenumber=0")]
        [InlineData("tags=cat&longitude=180&latitude=90&pagenumber=1&pagesize=0")]
        public async Task Images_Search_ValidatesImageFilter(string query)
        {
            var response = await Client.GetAsync($"/api/images?{query}");

            response.AssertStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Images_Search_CacheHit_ReturnsCachedValue()
        {
            const string id = "42";
            var cached = QueryResult<ImageSet>.Success(
                new ImageSet { Images = new[] { new Image { Id = id } } }
            );
            CachingServiceMock
                .Setup(x => x.GetAsync<QueryResult<ImageSet>>(It.IsAny<string>()))
                .ReturnsAsync(cached)
                .Verifiable();

            var response = await Client.GetAsync("/api/images?tags=cat");
            var model = await response.GetModelAsync<ImageSet>();

            response.AssertStatusCode(HttpStatusCode.OK);
            //todo: fluent validation
            Assert.Equal(id, model.Images.Single().Id);
            CachingServiceMock.Verify();
            ImageServiceMock
                .Verify(x => x.SearchAsync(It.IsAny<ImageFilter>()), Times.Never);
        }

        [Fact]
        public async Task Images_Search_CacheMiss_ReturnsStoredValue_CachesValue()
        {
            const string id = "42";
            var stored = QueryResult<ImageSet>.Success(
                new ImageSet { Images = new[] { new Image { Id = id } } }
            );
            ImageServiceMock
                .Setup(x => x.SearchAsync(It.IsAny<ImageFilter>()))
                .ReturnsAsync(stored)
                .Verifiable();

            var response = await Client.GetAsync("/api/images?tags=cat");
            var model = await response.GetModelAsync<ImageSet>();

            response.AssertStatusCode(HttpStatusCode.OK);
            //todo: fluent validation
            Assert.Equal(id, model.Images.Single().Id);
            ImageServiceMock.Verify();
            CachingServiceMock
                .Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<QueryResult<ImageSet>>()), Times.Once);
        }
    }
}
