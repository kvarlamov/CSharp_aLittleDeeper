using Microsoft.AspNetCore.Mvc;
using p21_MartenEx.Infrastructure;
using p21_MartenEx.Models;

namespace p21_MartenEx.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplateController : ControllerBase
{
    private readonly IRepository _repository;

    public TemplateController(IRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public ActionResult<List<Template>> GetAll()
    {
        return _repository.GetAllTemplates();
    }

    [HttpGet("{id}")]
    public ActionResult<Template> GetById(Guid id)
    {
        return _repository.GetTemplateById(id);
    }

    [HttpPost]
    public ActionResult Create(TemplateDto templateDto)
    {
        //todo - проблема - как сохранить для связанных сущностей TemplateId если мы еще не сохранили шаблон
        
        var securities = _repository.CreateSecurities(templateDto.Securities);
        var fieldSecurities = _repository.CreateFieldSecurities(templateDto.FieldSecurities);
        var someRules = _repository.CreateSomeRules(templateDto.SomeRules);

        var template = new Template()
        {
            Name = templateDto.Name,
            Securities = securities,
            FieldSecurities = fieldSecurities,
            SomeRules = someRules,
            Fields = templateDto.Fields
        };
        
        _repository.CreateTemplate(template);
        
        return Ok();
    }
    
    /*
     {
  "name": "Test",
  "fields": [
    {
      "name": "TestField",
      "kind": 1
    }
  ],
  "securities": [
    {
      "property1": "1",
      "property2": "2",
      "property3": "3",
      "property4": "4"
    }
  ],
  "fieldSecurities": [
    {
      "property1": "fieldP1",
      "property2": "fieldP2",
      "property3": "fieldP3",
      "property4": "fieldP4"
    }
  ],
  "someRules": [
    {
      "property1": "rule1",
      "property2": "rule2"
    }
  ]
}
     */
}