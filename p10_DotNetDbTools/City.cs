using System.Collections.Generic;

namespace p10_DotNetDbTools
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<CityToCategory> CityToCategories { get; set; }

        public City()
        {
            CityToCategories = new List<CityToCategory>();
        }
    }
}