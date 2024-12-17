using StackExchange.Redis;
using University.User.Application.IServices;

namespace University.User.ApplicationService.Services;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;
   

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }



    public async Task<string> GetValueAsynck(string key)
    {
        var redisDB = _redis.GetDatabase();
        var value = await redisDB.StringGetAsync(key);
        return value.ToString();
    }

    public async Task SetValueAsynck(string key , string value)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, value);
    }


}
