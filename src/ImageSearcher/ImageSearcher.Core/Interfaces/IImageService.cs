using ImageSearcher.Core.DTO;

namespace ImageSearcher.Core.Interfaces
{
    public interface IImageService
    {
        QueryResult<Image> GetById(string id);
        QueryResult<ImageSet> Search(ImageFilter filter);
    }
}
