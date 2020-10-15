using System.Collections.Generic;

namespace p10_DotNetDbTools
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<CityToCategory> CityToCategories { get; set; }

        public Category()
        {
            CityToCategories = new List<CityToCategory>();
        }
    }
}