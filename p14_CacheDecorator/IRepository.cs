namespace p14_CacheDecorator;

public interface IRepository
{
    Item? GetITem(int id);
}

public class InMemoryRepository : IRepository
{
    private readonly List<Item> items;

    public InMemoryRepository()
    {
        items = new List<Item>()
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
    
    public Item? GetITem(int id)
    {
        return items.FirstOrDefault(item => item.Id == id);
    }
}

public class Item
{
    public Item(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public int Id { get; set; }

    public string Name { get; set; }
}