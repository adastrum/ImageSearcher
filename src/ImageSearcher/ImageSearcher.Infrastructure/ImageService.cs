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

        public async Task<QueryResult<ImageInfo>> GetById(string id)
        {
            try
            {
                var query = await BuildQueryString(id, GetInfoMethod, _apiKey);

                var uriBuilder = new UriBuilder(FlickrApiAddress)
                {
                    Query = query
                };
                var uri = uriBuilder.ToString();

                var content = await _client.GetStringAsync(uri);

                var model = JsonConvert.DeserializeObject<GetByIdResponse>(content);

                var code = MapFlickrErrorCode(model.code);
                if (code != HttpStatusCode.OK)
                {
                    return QueryResult<ImageInfo>.Fail(code, model.message);
                }

                var imageInfo = Mapper.Map<ImageInfo>(model.photo);

                return QueryResult<ImageInfo>.Success(imageInfo);
            }
            catch (HttpRequestException exception)
            {
                //todo
                return QueryResult<ImageInfo>.Fail(HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception)
            {
                return QueryResult<ImageInfo>.Fail(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        public async Task<QueryResult<ImageSet>> Search(ImageFilter filter)
        {
            throw new NotImplementedException();
        }

        private static async Task<string> BuildQueryString(string id, string method, string apiKey)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"method", method},
                {"api_key", apiKey},
                {"photo_id", id},
                {"format", "json"},
                {"nojsoncallback", "1"}
            };

            using (var content = new FormUrlEncodedContent(queryParams))
            {
                return await content.ReadAsStringAsync();
            }
        }

        private HttpStatusCode MapFlickrErrorCode(int flickrErrorCode)
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
