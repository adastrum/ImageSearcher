using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private const string RegionAccuracy = "6";

        private readonly IHttpHandler _httpHandler;
        private readonly string _apiKey;

        public ImageService(IHttpHandler httpHandler, string apiKey)
        {
            _httpHandler = httpHandler;
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

            return await GetResultAsync<GetByIdResponse, ImageInfo>(query, x => Mapper.Map<ImageInfo>(x.photo));
        }

        public async Task<QueryResult<ImageSet>> SearchAsync(ImageFilter filter)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"method", SearchMethod},
                {"api_key", _apiKey}
            };

            if (filter.Tags.Any())
            {
                queryParams["tags"] = string.Join(",", filter.Tags);
            }

            if (filter.Longitude.HasValue)
            {
                queryParams["lon"] = filter.Longitude.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (filter.Latitude.HasValue)
            {
                queryParams["lat"] = filter.Latitude.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (filter.Longitude.HasValue || filter.Latitude.HasValue)
            {
                queryParams["accuracy"] = RegionAccuracy;
            }

            var query = await BuildQueryStringAsync(queryParams);

            return await GetResultAsync<SearchResponse, ImageSet>(
                query,
                x => new ImageSet
                {
                    PageNumber = x.photos.page,
                    PageSize = x.photos.perpage,
                    Images = x.photos.photo.Select(Mapper.Map<Image>)
                });
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

        private async Task<QueryResult<TModel>> GetResultAsync<TResponse, TModel>(string query, Func<TResponse, TModel> mapping)
            where TResponse : FlickrResponseBase
            where TModel : class
        {
            try
            {
                var uriBuilder = new UriBuilder(FlickrApiAddress)
                {
                    Query = query
                };
                var uri = uriBuilder.ToString();

                var content = await _httpHandler.GetStringAsync(uri);

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
