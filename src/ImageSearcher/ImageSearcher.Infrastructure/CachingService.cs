using System;
using System.Threading.Tasks;
using ImageSearcher.Core.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ImageSearcher.Infrastructure
{
    public class CachingService : ICachingService
    {
        private readonly IDatabase _database;

        public CachingService(IDatabase database)
        {
            _database = database;
        }

        public async Task SetAsync<TValue>(string key, TValue value)
            where TValue : class
        {
            var serialized = JsonConvert.SerializeObject(value);
            await _database.StringSetAsync(key, serialized);
        }

        public async Task<TValue> GetAsync<TValue>(string key)
            where TValue : class
        {
            try
            {
                var serialized = await _database.StringGetAsync(key);
                return JsonConvert.DeserializeObject<TValue>(serialized);
            }
            catch (Exception exception)
            {
                //todo: log
                return await Task.FromResult<TValue>(null);
            }
        }
    }
}
