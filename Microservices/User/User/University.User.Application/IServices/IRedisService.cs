namespace University.User.Application.IServices;

public interface IRedisService
{
    Task<string> GetValueAsync(string key);
    Task SetValueAsync(string key, string value , TimeSpan? expiry = null);
}
