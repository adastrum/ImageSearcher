using System.Collections.Generic;

namespace ImageSearcher.Infrastructure.Models
{
    public class Photos
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public string total { get; set; }
        public List<Photo> photo { get; set; }
    }

    public class SearchResponse : FlickrResponseBase
    {
        public Photos photos { get; set; }
        public string stat { get; set; }
    }
}
