namespace p21_MartenEx.Models;

public class TemplateDto
{
    public string Name { get; set; }
    public List<Field> Fields { get; set; }

    public List<Security> Securities { get; set; }
    
    public List<FieldSecurity> FieldSecurities { get; set; }
    
    public List<SomeRule> SomeRules { get; set; }
}