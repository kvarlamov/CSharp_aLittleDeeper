namespace p21_MartenEx.Models;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
}