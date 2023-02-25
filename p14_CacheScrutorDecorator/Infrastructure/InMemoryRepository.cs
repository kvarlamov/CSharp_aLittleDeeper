namespace p15_CacheScrutorDecorator.Infrastructure;

public class InMemoryRepository : IRepository
{
    private readonly List<Item> _items;

    public InMemoryRepository()
    {
        _items = new List<Item>()
        {
            new Item(1, "1"),
            new Item(2, "2"),
            new Item(3, "3"),
            new Item(4, "4"),
            new Item(5, "5"),
            new Item(6, "6"),
            new Item(7, "7"),
        };
    }

    public Item? GetItem(int? id)
    {
        // imitation of long work to compare with getting from cache
        Thread.Sleep(2000);
        return _items.FirstOrDefault(item => item.Id == id);
    }
}