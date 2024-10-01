using EAccountingServer.Application.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace EAccountingServer.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public T? Get<T>(string key)
        {
            var value = _database.StringGet(key);
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<T>(value.ToString());
            }

            return default(T?);
        }

        public bool Remove(string key)
        {
            return _database.KeyDelete(key);
        }

        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            _database.StringSet(key, serializedValue, expiry ?? TimeSpan.FromHours(1));
        }
    }
}
