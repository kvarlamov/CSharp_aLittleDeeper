using System.ComponentModel.DataAnnotations.Schema;

namespace p10_DotNetDbTools
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        // public int Age { get; set; }
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}