using System.Threading.Tasks;

namespace ImageSearcher.Core.Interfaces
{
    public interface ICachingService
    {
        Task SetAsync<TValue>(string key, TValue value)
            where TValue : class;

        Task<TValue> GetAsync<TValue>(string key)
            where TValue: class;
    }
}
