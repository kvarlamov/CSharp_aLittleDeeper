using p15_CacheScrutorDecorator.Infrastructure;

namespace p15_CacheScrutorDecorator.Services;

public class ItemService : IItemService
{
    private readonly IRepository _repository;

    public ItemService(IRepository repository)
    {
        _repository = repository;
    }

    public Item? GetITem(int? id) =>
        _repository.GetItem(id);
}

public interface IItemService
{
    Item? GetITem(int? id);
}