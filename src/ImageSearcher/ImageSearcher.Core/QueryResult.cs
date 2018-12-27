using System.Net;

namespace ImageSearcher.Core
{
    public class QueryResult<TModel>
    {
        //todo: custom enum
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public TModel Model { get; set; }
    }
}
