using System.Threading.Tasks;
using ImageSearcher.Core.DTO;

namespace ImageSearcher.Core.Interfaces
{
    public interface IImageService
    {
        Task<QueryResult<ImageInfo>> GetById(string id);
        Task<QueryResult<ImageSet>> Search(ImageFilter filter);
    }
}
