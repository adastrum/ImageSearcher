using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ImageSearcher.Core.DTO;
using ImageSearcher.Core.Interfaces;
using ImageSearcher.Infrastructure.MapperProfiles;
using ImageSearcher.Infrastructure.Tests.Properties;
using Moq;
using Xunit;

namespace ImageSearcher.Infrastructure.Tests
{
    public class ImageServiceTests : IDisposable
    {
        private const string ApiKey = "bdda66b438fb8008ed3e70a4f8e4e5b0";
        private const string Id = "46482105201";

        private readonly IMapper _mapper;

        public ImageServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SearchProfile>();
                cfg.AddProfile<GetByIdProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact(Skip = "live test")]
        public async Task GetById_GetsImageInfo_Live()
        {
            var client = new HttpClient();
            var httpHandler = new HttpHandler(client);
            var sut = new ImageService(httpHandler, ApiKey, _mapper);

            var queryResult = await sut.GetByIdAsync(Id);

            Assert.Equal(HttpStatusCode.OK, queryResult.Code);
            Assert.Null(queryResult.Message);
            Assert.NotNull(queryResult.Model);
        }

        [Fact]
        public async Task GetById_GetsImageInfo()
        {
            var uri = $"https://api.flickr.com:443/services/rest?format=json&nojsoncallback=1&method=flickr.photos.getInfo&api_key={ApiKey}&photo_id={Id}";
            var httpHandlerMock = new Mock<IHttpHandler>();
            httpHandlerMock
                .Setup(x => x.GetStringAsync(uri))
                .ReturnsAsync(Resources.GetInfoSampleResponse);
            var sut = new ImageService(httpHandlerMock.Object, ApiKey, _mapper);

            var queryResult = await sut.GetByIdAsync(Id);

            Assert.Equal(HttpStatusCode.OK, queryResult.Code);
            Assert.Null(queryResult.Message);
            Assert.NotNull(queryResult.Model);
        }

        [Fact]
        public async Task SearchAsync_GetsImagesByFilter()
        {
            var uri = $"https://api.flickr.com:443/services/rest?format=json&nojsoncallback=1&method=flickr.photos.search&api_key={ApiKey}&tags=cat";
            var httpHandlerMock = new Mock<IHttpHandler>();
            httpHandlerMock
                .Setup(x => x.GetStringAsync(uri))
                .ReturnsAsync(Resources.SearchSampleResponse);
            var sut = new ImageService(httpHandlerMock.Object, ApiKey, _mapper);
            var filter = new ImageFilter { Tags = new[] { "cat" } };

            var queryResult = await sut.SearchAsync(filter);

            Assert.Equal(HttpStatusCode.OK, queryResult.Code);
            Assert.Null(queryResult.Message);
            Assert.NotNull(queryResult.Model);
        }

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}
