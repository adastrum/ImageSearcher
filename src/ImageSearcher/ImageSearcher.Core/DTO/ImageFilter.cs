using System.Collections.Generic;

namespace ImageSearcher.Core.DTO
{
    public class ImageFilter
    {
        public ImageFilter()
        {
            Tags = new List<string>();
        }

        public IEnumerable<string> Tags { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
