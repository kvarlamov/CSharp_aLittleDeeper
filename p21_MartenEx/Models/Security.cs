namespace p21_MartenEx.Models;

public class Security : BaseEntity<Guid>
{
    public string Property1 { get; set; }
    
    public string Property2 { get; set; }
    
    public string Property3 { get; set; }
    
    public string Property4 { get; set; }
    
    public Guid TemplateId { get; set; }
}