using ImageSearcher.Core.DTO;
using ImageSearcher.Infrastructure.Models;

namespace ImageSearcher.Infrastructure.MapperProfiles
{
    public class SearchProfile : FlickrProfileBase
    {
        public SearchProfile()
        {
            CreateMap<Photo, Image>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.id))
                .ForMember(x => x.Owner, x => x.MapFrom(y => y.owner))
                .ForMember(x => x.Secret, x => x.MapFrom(y => y.secret))
                .ForMember(x => x.Server, x => x.MapFrom(y => y.server))
                .ForMember(x => x.Farm, x => x.MapFrom(y => y.farm))
                .ForMember(x => x.Title, x => x.MapFrom(y => y.title))
                .ForMember(x => x.IsPublic, x => x.MapFrom(y => y.ispublic == FlickrApiTrue))
                .ForMember(x => x.IsFriend, x => x.MapFrom(y => y.isfriend == FlickrApiTrue))
                .ForMember(x => x.IsFamily, x => x.MapFrom(y => y.isfamily == FlickrApiTrue));
        }
    }
}
