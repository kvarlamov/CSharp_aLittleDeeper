using p15_EFplusDapper.Infrastructure.Entities;

namespace p15_EFplusDapper.Infrastructure;

public interface IUserRepository
{
    Task<User> GetById(long id);

    Task<List<User>> GetAll();

    Task<int> Create(User user);
}