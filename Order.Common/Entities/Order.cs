using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Entities
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
