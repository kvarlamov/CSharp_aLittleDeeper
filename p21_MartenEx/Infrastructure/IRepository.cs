using Microsoft.EntityFrameworkCore;
using p21_MartenEx.Models;

namespace p21_MartenEx.Infrastructure;

public interface IRepository
{
    Template GetTemplateById(Guid id);

    List<Template> GetAllTemplates();

    void CreateTemplate(Template template);

    List<Guid> CreateSecurities(List<Security> securities);
    
    List<Guid> CreateFieldSecurities(List<FieldSecurity> fieldSecurities);
    
    List<Guid> CreateSomeRules(List<SomeRule> someRules);
}