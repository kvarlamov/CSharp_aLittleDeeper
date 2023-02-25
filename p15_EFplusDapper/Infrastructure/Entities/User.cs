namespace p15_EFplusDapper.Infrastructure.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public int Age { get; set; }

    public List<Address> Address { get; set; }
}

public class Address : BaseEntity
{
    public string? City { get; set; }

    public string? Street { get; set; }

    public string? FlatNumBer { get; set; }

    public string? AdditionalInfo { get; set; }
    
    public User User { get; set; }
}