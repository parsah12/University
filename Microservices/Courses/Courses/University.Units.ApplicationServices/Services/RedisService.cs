using StackExchange.Redis;
using University.Course.Application.IServices;

namespace University.Course.ApplicationServices.Services;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<string> GetValueAsync(string key)
    {
        var db = _redis.GetDatabase();
        var value = $"course:{key}:name";
        var res = await db.StringGetAsync(value);
        return res.ToString();
    }

    public async Task SetValueAsync(string key, string value , TimeSpan? expiry = null)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, value);
    }
}
