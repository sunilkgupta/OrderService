using Microsoft.EntityFrameworkCore;

namespace Order.DataContext
{
    public class OrderAPIContext : DbContext
    {
        public OrderAPIContext(DbContextOptions<OrderAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Common.Entities.Order> Order { get; set; } = default!;
    }
}
