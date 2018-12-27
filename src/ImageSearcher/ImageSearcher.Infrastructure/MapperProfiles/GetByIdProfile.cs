using ImageSearcher.Core.DTO;
using ImageSearcher.Infrastructure.Models;

namespace ImageSearcher.Infrastructure.MapperProfiles
{
    public class GetByIdProfile : FlickrProfileBase
    {
        public GetByIdProfile()
        {
            CreateMap<PhotoInfo, ImageInfo>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.id))
                .ForMember(x => x.Owner, x => x.MapFrom(y => y.owner == null ? null : y.owner.username))
                .ForMember(x => x.Secret, x => x.MapFrom(y => y.secret))
                .ForMember(x => x.Server, x => x.MapFrom(y => y.server))
                .ForMember(x => x.Farm, x => x.MapFrom(y => y.farm))
                .ForMember(x => x.Title, x => x.MapFrom(y => y.title == null ? null : y.title._content))
                .ForMember(x => x.IsPublic, x => x.MapFrom(y => y.visibility != null && y.visibility.ispublic == FlickrApiTrue))
                .ForMember(x => x.IsFriend, x => x.MapFrom(y => y.visibility != null && y.visibility.isfriend == FlickrApiTrue))
                .ForMember(x => x.IsFamily, x => x.MapFrom(y => y.visibility != null && y.visibility.isfamily == FlickrApiTrue))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
