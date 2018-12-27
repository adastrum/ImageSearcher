using System.Threading.Tasks;

namespace ImageSearcher.Core.Interfaces
{
    public interface IHttpHandler
    {
        Task<string> GetStringAsync(string uri);
    }
}
