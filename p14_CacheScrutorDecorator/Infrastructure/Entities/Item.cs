namespace p15_CacheScrutorDecorator.Infrastructure;

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