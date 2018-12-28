using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace ImageSearcher.Web.Api.Tests
{
    public static class ResponseExtension
    {
        public static async Task<TModel> GetModelAsync<TModel>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TModel>(content);
        }

        public static void AssertStatusCode(this HttpResponseMessage response, HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, response.StatusCode);
        }
    }
}
