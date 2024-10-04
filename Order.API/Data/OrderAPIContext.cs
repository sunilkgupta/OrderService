using Microsoft.EntityFrameworkCore;

namespace OrderService.Data
{
    public class OrderAPIContext : DbContext
    {
        public OrderAPIContext(DbContextOptions<OrderAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Order.API.Models.Order> Order { get; set; } = default!;
    }
}
