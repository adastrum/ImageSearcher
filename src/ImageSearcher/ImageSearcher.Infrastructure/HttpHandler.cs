using System.Net.Http;
using System.Threading.Tasks;
using ImageSearcher.Core.Interfaces;

namespace ImageSearcher.Infrastructure
{
    public class HttpHandler : IHttpHandler
    {
        private readonly HttpClient _client;

        public HttpHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetStringAsync(string uri)
        {
            return await _client.GetStringAsync(uri);
        }
    }
}
