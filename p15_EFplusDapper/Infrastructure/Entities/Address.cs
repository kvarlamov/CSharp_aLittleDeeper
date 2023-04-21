namespace p15_EFplusDapper.Infrastructure.Entities;

public class Address : BaseEntity
{
    public string? City { get; set; }

    public string? Street { get; set; }

    public string? FlatNumBer { get; set; }

    public string? AdditionalInfo { get; set; }
    
    public User User { get; set; }
}