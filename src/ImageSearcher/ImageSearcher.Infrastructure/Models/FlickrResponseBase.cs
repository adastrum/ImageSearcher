namespace ImageSearcher.Infrastructure.Models
{
    public abstract class FlickrResponseBase
    {
        public string stat { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
}
