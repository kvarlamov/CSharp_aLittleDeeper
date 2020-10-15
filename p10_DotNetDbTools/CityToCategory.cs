using System.ComponentModel.DataAnnotations.Schema;

namespace p10_DotNetDbTools
{
    public class CityToCategory
    {
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}