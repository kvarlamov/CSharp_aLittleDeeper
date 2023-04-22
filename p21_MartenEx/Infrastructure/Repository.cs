using Marten;
using p21_MartenEx.Models;

namespace p21_MartenEx.Infrastructure;

public class Repository : IRepository
{
    private readonly DatabaseContext _db;
    private readonly IDocumentSession _session;

    public Repository(DatabaseContext db, IDocumentSession session)
    {
        _db = db;
        _session = session;
    }
    
    public Template GetTemplateById(Guid id)
    {
        return _session.Query<Template>().Single(x => x.Id == id);
    }

    public List<Template> GetAllTemplates()
    {
        return _session.Query<Template>().ToList();
    }

    public void CreateTemplate(Template template)
    {
        _session.Store(template);
        _session.SaveChanges();
    }

    public List<Guid> CreateSecurities(List<Security> securities)
    {
        _db.Securities.AddRange(securities);
        _db.SaveChanges();

        return securities.Select(x => x.Id).ToList();
    }

    public List<Guid> CreateFieldSecurities(List<FieldSecurity> fieldSecurities)
    {
        _db.FieldSecurities.AddRange(fieldSecurities);
        _db.SaveChanges();

        return fieldSecurities.Select(x => x.Id).ToList();
    }

    public List<Guid> CreateSomeRules(List<SomeRule> someRules)
    {
        _db.SomeRules.AddRange(someRules);
        _db.SaveChanges();

        return someRules.Select(x => x.Id).ToList();
    }
}