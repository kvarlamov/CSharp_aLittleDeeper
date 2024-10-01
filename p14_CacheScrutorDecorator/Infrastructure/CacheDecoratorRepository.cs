using Microsoft.Extensions.Caching.Memory;

namespace p15_CacheScrutorDecorator.Infrastructure;

public class CacheDecoratorRepository : IRepository
{
    //todo - move expirationSeconds to settings
    private const int SlidingExpirationSeconds = 10;
    private const int AbsoluteExpirationSeconds = 60;
    private readonly IRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheOptions;

    //todo - inject cache options
    public CacheDecoratorRepository(IRepository repository, IMemoryCache cache)//, MemoryCacheEntryOptions cacheOptions)
    {
        _repository = repository;
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(SlidingExpirationSeconds))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(AbsoluteExpirationSeconds));
    }
    
    public Item? GetItem(int? id)
    {
        return _cache.GetOrCreate(id, entry =>
        {
            entry.SetOptions(_cacheOptions);
            return _repository.GetItem(id);
        });
    }
}