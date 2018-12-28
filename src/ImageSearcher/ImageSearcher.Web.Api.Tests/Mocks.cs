using System;
using System.Threading.Tasks;
using ImageSearcher.Core;
using ImageSearcher.Core.DTO;
using ImageSearcher.Core.Interfaces;

namespace ImageSearcher.Web.Api.Tests
{
    //todo: fix version conflicts and use Moq
    public class TestImageService : IImageService
    {
        public Task<QueryResult<ImageInfo>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<ImageSet>> SearchAsync(ImageFilter filter)
        {
            throw new NotImplementedException();
        }
    }

    public class TestCachingService : ICachingService
    {
        public Task SetAsync<TValue>(string key, TValue value) where TValue : class
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetAsync<TValue>(string key) where TValue : class
        {
            throw new NotImplementedException();
        }
    }

    public class TestHttpHandler : IHttpHandler
    {
        public Task<string> GetStringAsync(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
