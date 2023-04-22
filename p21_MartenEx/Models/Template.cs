namespace p21_MartenEx.Models;

public class Template : BaseEntity<Guid>
{
    public string Name { get; set; }
    public List<Field> Fields { get; set; }

    public List<Guid> Securities { get; set; }
    
    public List<Guid> FieldSecurities { get; set; }
    
    public List<Guid> SomeRules { get; set; }
}