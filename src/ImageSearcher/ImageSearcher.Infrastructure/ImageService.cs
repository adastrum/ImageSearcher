using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ImageSearcher.Core;
using ImageSearcher.Core.DTO;
using ImageSearcher.Core.Interfaces;
using ImageSearcher.Infrastructure.Models;
using Newtonsoft.Json;

namespace ImageSearcher.Infrastructure
{
    public class ImageService : IImageService
    {
        //todo: configuration via constructor
        private const string FlickrApiAddress = "https://api.flickr.com/services/rest";
        private const string SearchMethod = "flickr.photos.search";
        private const string GetInfoMethod = "flickr.photos.getInfo";

        private readonly HttpClient _client;
        private readonly string _apiKey;

        public ImageService(HttpClient client, string apiKey)
        {
            _client = client;
            _apiKey = apiKey;
        }

        public async Task<QueryResult<ImageInfo>> GetByIdAsync(string id)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"method", GetInfoMethod},
                {"api_key", _apiKey},
                {"photo_id", id}
            };

            var query = await BuildQueryStringAsync(queryParams);

            var uriBuilder = new UriBuilder(FlickrApiAddress)
            {
                Query = query
            };
            var uri = uriBuilder.ToString();

            return await GetResultAsync<GetByIdResponse, ImageInfo>(uri, x => Mapper.Map<ImageInfo>(x.photo));
        }

        public async Task<QueryResult<ImageSet>> SearchAsync(ImageFilter filter)
        {
            throw new NotImplementedException();
        }

        private static async Task<string> BuildQueryStringAsync(Dictionary<string, string> extraParams)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"format", "json"},
                {"nojsoncallback", "1"}
            };

            foreach (var param in extraParams)
            {
                queryParams.TryAdd(param.Key, param.Value);
            }

            using (var content = new FormUrlEncodedContent(queryParams))
            {
                return await content.ReadAsStringAsync();
            }
        }

        private async Task<QueryResult<TModel>> GetResultAsync<TResponse, TModel>(string uri, Func<TResponse, TModel> mapping)
            where TResponse : FlickrResponseBase
            where TModel : class
        {
            try
            {
                var content = await _client.GetStringAsync(uri);

                var response = JsonConvert.DeserializeObject<TResponse>(content);

                var code = MapFlickrErrorCode(response.code);
                if (code != HttpStatusCode.OK)
                {
                    return QueryResult<TModel>.Fail(code, response.message);
                }

                var model = mapping(response);

                return QueryResult<TModel>.Success(model);
            }
            catch (HttpRequestException exception)
            {
                //todo
                return QueryResult<TModel>.Fail(HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception)
            {
                return QueryResult<TModel>.Fail(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        private static HttpStatusCode MapFlickrErrorCode(int flickrErrorCode)
        {
            switch (flickrErrorCode)
            {
                case 1:
                    return HttpStatusCode.NotFound;
                case 100:
                    return HttpStatusCode.BadRequest;
                case 105:
                    return HttpStatusCode.InternalServerError;
                case 106:
                    return HttpStatusCode.InternalServerError;
                case 111:
                    return HttpStatusCode.BadRequest;
                case 112:
                    return HttpStatusCode.BadRequest;
                case 114:
                    return HttpStatusCode.BadRequest;
                case 115:
                    return HttpStatusCode.BadRequest;
                case 116:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.OK;
            }
        }
    }
}
