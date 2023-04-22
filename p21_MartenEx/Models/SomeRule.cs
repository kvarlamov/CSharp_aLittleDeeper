namespace p21_MartenEx.Models;

public class SomeRule : BaseEntity<Guid>
{
    public string Property1 { get; set; }
    
    public string Property2 { get; set; }
    
    public Guid TemplateId { get; set; }
}