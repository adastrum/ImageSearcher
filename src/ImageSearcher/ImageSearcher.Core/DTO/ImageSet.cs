using System.Collections.Generic;

namespace ImageSearcher.Core.DTO
{
    public class ImageSet
    {
        public ImageSet()
        {
            Images = new List<Image>();
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
