using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ImageSearcher.Infrastructure.MapperProfiles;
using Xunit;

namespace ImageSearcher.Infrastructure.Tests
{
    public class ImageServiceTests
    {
        public ImageServiceTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<SearchProfile>();
                cfg.AddProfile<GetByIdProfile>();
            });
        }

        [Fact(Skip = "live test")]
        public async Task GetById_GetsImageInfo()
        {
            var client = new HttpClient();
            const string apiKey = "bdda66b438fb8008ed3e70a4f8e4e5b0";
            var sut = new ImageService(client, apiKey);

            var queryResult = await sut.GetByIdAsync("46482105201");

            Assert.Equal(HttpStatusCode.OK, queryResult.Code);
            Assert.Null(queryResult.Message);
            Assert.NotNull(queryResult.Model);
        }
    }
}
