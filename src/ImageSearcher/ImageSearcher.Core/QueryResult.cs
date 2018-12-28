using System.Net;

namespace ImageSearcher.Core
{
    public class QueryResult<TModel>
        where TModel : class
    {
        protected QueryResult()
        {
            
        }

        protected QueryResult(TModel model, HttpStatusCode code, string message)
        {
            Model = model;
            Code = code;
            Message = message;
        }

        public static QueryResult<TModel> Success(TModel model)
        {
            return new QueryResult<TModel>(model, HttpStatusCode.OK, null);
        }

        public static QueryResult<TModel> Fail(HttpStatusCode code, string message)
        {
            return new QueryResult<TModel>(null, code, message);
        }

        //todo: custom enum
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public TModel Model { get; set; }
    }
}
