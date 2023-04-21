namespace p15_EFplusDapper.Infrastructure.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public int Age { get; set; }

    public List<Address> Address { get; set; }
}