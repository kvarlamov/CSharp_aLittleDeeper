using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace p10_DotNetDbTools
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int Price { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime StartSaleTime { get; set; }
        public DateTime EndSaleTime { get; set; }
    }
}