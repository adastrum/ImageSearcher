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

        public ImagesController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _imageService.GetByIdAsync(id);

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

            var result = await _imageService.SearchAsync(filter);

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
