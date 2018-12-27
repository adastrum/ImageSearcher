using System.Net;
using System.Threading.Tasks;
using ImageSearcher.Core;
using ImageSearcher.Core.DTO;
using ImageSearcher.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageSearcher.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var result = await _imageService.GetByIdAsync(id);

            return GetActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]ImageFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
