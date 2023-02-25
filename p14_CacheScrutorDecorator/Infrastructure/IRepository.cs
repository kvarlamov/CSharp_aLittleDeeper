namespace p15_CacheScrutorDecorator.Infrastructure;

public interface IRepository
{
    Item? GetItem(int? id);
}