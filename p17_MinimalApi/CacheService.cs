using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace p17_MinimalApi;

public class CacheService : ICacheService
{
    private readonly IDatabase _db;

    public CacheService()
    {
        _db = RedisConnectionHelper.Connection.GetDatabase();
    }
    
    public async Task<TItem?> GetValueAsync<TItem>(string key)
    {
        var value = await _db.StringGetAsync(key);
        if (!string.IsNullOrEmpty(value))
            return JsonSerializer.Deserialize<TItem>(value);

        return default;
    }

    public async Task<bool> SetValueAsync<TItem>(string key, TItem value)
    {
        var json = JsonSerializer.Serialize(value);
        var isSet = await _db.StringSetAsync(key, json, TimeSpan.FromSeconds(300));

        return isSet;
    }
}

public interface ICacheService
{
    Task<TItem?> GetValueAsync<TItem>(string key);

    Task<bool> SetValueAsync<TItem>(string key, TItem value);
}