namespace p15_EFplusDapper.Models;

public sealed record UserDto(string FirstName, string LastName, int Age, List<AddressDto> Adresses);