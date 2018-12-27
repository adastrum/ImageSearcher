using System.Threading.Tasks;
using ImageSearcher.Core.DTO;

namespace ImageSearcher.Core.Interfaces
{
    public interface IImageService
    {
        Task<QueryResult<ImageInfo>> GetByIdAsync(string id);
        Task<QueryResult<ImageSet>> SearchAsync(ImageFilter filter);
    }
}
