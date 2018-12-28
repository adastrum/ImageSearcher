using AutoMapper;
using ImageSearcher.Core.DTO;
using ImageSearcher.Web.Api.Models.Request;

namespace ImageSearcher.Web.Api.Models.MapperProfiles
{
    public class ImageFilterProfile : Profile
    {
        public ImageFilterProfile()
        {
            CreateMap<SearchImages, ImageFilter>();
        }
    }
}
