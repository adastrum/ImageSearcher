using AutoMapper;
using ImageSearcher.Core.DTO;
using ImageSearcher.Infrastructure.Models;

namespace ImageSearcher.Infrastructure.MapperProfiles
{
    public class GetByIdProfile : Profile
    {
        public GetByIdProfile()
        {
            CreateMap<Photo, ImageInfo>()
                .IncludeBase<Photo, Image>()
                //todo
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
