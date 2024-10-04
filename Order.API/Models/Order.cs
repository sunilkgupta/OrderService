using System.ComponentModel.DataAnnotations;

namespace Order.API.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        public string? CustomerName { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
