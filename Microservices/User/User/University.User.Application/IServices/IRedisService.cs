namespace University.User.Application.IServices;

public interface IRedisService
{
    Task<string> GetValueAsynck(string key);
    Task SetValueAsynck (string key, string value);

    //Task<T> GetAsynck<T>(string key);
    //Task SetAsynck<T>(string key, T value , TimeSpan? expiry = null);
}
