using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace p10_DotNetDbTools
{
    public class Deal
    {
        public int Id { get; set; }
        public int? SellerId { get; set; }
        [ForeignKey("SellerId")]
        public User Seller { get; set; } 
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public DateTime DealTime { get; set; }
    }
}