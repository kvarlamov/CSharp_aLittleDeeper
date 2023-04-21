using Microsoft.AspNetCore.Mvc;
using p15_EFplusDapper.Infrastructure;
using p15_EFplusDapper.Infrastructure.Entities;
using p15_EFplusDapper.Models;

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

    [HttpGet("~/Test")]
    public string GetTest()
    {
        return "GetTest()";
    }

    [HttpGet("{id:long}", Name = "GetById")]
    public async Task<User> GetUserById(long id)
    {
        return await _userRepository.GetById(id);
    }

    [HttpPost]
    public async Task<int> Create(UserDto userDto)
    {
        //todo - add automapper
        var adresses = new List<Address>();

        if (userDto.Adresses is {Count: > 0})
        {
            foreach (var adressDto in userDto.Adresses)
            {
                adresses.Add(new Address()
                {
                    City = adressDto.City,
                    Street = adressDto.Street,
                    FlatNumBer = adressDto.FlatNumber,
                    AdditionalInfo = adressDto.AdditionalInfo
                });
            }
        }

        //todo - skipping address, implement in repository
        User newUser = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Age = userDto.Age,
            Address = adresses
        };

        var createdUserId = await _userRepository.Create(newUser);

        return createdUserId;
    }
}