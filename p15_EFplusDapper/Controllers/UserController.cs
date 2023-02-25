using Microsoft.AspNetCore.Mvc;
using p15_EFplusDapper.Infrastructure;
using p15_EFplusDapper.Infrastructure.Entities;

namespace p15_EFplusDapper.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet(Name = "GetAll")]
    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    [HttpGet("{id:long}", Name = "GetById")]
    public async Task<User> GetUserById(long id)
    {
        return await _userRepository.GetById(id);
    }
}