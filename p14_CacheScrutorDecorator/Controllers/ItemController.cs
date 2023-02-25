using Microsoft.AspNetCore.Mvc;
using p15_CacheScrutorDecorator.Infrastructure;
using p15_CacheScrutorDecorator.Services;

namespace p15_CacheScrutorDecorator.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : Controller
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet(Name = "GetItem")]
    public Item? GetById(int? id)
    {
        return _itemService.GetITem(id);
    }
}