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
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
}
