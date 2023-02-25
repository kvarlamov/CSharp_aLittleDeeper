using p15_EFplusDapper.Infrastructure.Entities;

namespace p15_EFplusDapper.Infrastructure.Data;

public static class FakeDataFactory
{
    public static List<User> GetUsers() =>
        new()
        {
            new User
            {
                Age = 20,
                FirstName = "Ivan",
                LastName = "Taranv",
                Address = new List<Address>
                {
                    new()
                    {
                        Street = "testStreet",
                        City = "testCity",
                        FlatNumBer = "1a"
                    }
                }
            }
        };
}    