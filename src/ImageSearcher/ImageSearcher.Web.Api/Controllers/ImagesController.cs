using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ImageSearcher.Core;
using ImageSearcher.Core.DTO;
using ImageSearcher.Core.Interfaces;
using ImageSearcher.Web.Api.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ImageSearcher.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly ICachingService _cache;

        public ImagesController(IImageService imageService, IMapper mapper, ICachingService cache)
        {
            _imageService = imageService;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _cache.GetAsync<QueryResult<ImageInfo>>(id);
            if (result == null)
            {
                result = await _imageService.GetByIdAsync(id);
                await _cache.SetAsync(id, result);
            }

            return GetActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery]SearchImages model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filter = _mapper.Map<ImageFilter>(model);

            var key = $"{string.Join(",", filter.Tags)}:{filter.Longitude}:{filter.Latitude}:{filter.PageNumber}:{filter.PageSize}";

            var result = await _cache.GetAsync<QueryResult<ImageSet>>(key);
            if (result == null)
            {
                result = await _imageService.SearchAsync(filter);
                await _cache.SetAsync(key, result);
            }

            return GetActionResult(result);
        }

        private ActionResult GetActionResult<TModel>(QueryResult<TModel> result)
            where TModel : class
        {
            return result.Code == HttpStatusCode.OK
                ? Ok(result.Model)
                : StatusCode((int)result.Code, result.Message);
        }
    }
}
