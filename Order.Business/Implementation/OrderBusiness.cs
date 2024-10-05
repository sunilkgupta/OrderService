using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Business.Interfaces;
using Order.DataContext;

namespace Order.Business.Implementation
{
    /// <summary>
    /// Order Business
    /// </summary>
    public class OrderBusiness : IOrderBusiness
    {
        private readonly OrderAPIContext _context;
        private readonly ILogger<OrderBusiness> _logger;
        public OrderBusiness(OrderAPIContext context, ILogger<OrderBusiness> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOrder(Guid id)
        {
            _logger.LogInformation("Order deleting, Id is {0}", id);
            try
            {
                var order = await _context.Order.FindAsync(id);
                if (order == null)
                {
                    _logger.LogWarning("Order Id is {0} null", id);
                    return false;
                }
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order deleted successfully, Id is {0}", id);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Common.Entities.Order>> GetOrders()
        {
            _logger.LogInformation("All Orders requested");
            try
            {
                var result = await _context.Order.ToListAsync();
                if (result == null || result.Count == 0)
                {
                    _logger.LogWarning("No Orders exists!");
                    return new List<Common.Entities.Order>();
                }
                return await _context.Order.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Get orders by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Common.Entities.Order?> GetOrdersById(Guid id)
        {
            _logger.LogInformation("Order requested: Id is {0}", id);
            try
            {
                var order = await _context.Order.FindAsync(id);

                if (order == null)
                {
                    _logger.LogWarning("No Orders with this {0} exists!", id);
                    return null;
                }
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Common.Entities.Order?> CreateOrder(Common.Entities.Order order)
        {
            _logger.LogInformation("New order creating..");
            try
            {                
                _context.Order.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Update existing order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task UpdateOrder(Guid id, Common.Entities.Order order)
        {
            _logger.LogInformation("Order updating: Id is {0}", id);
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var result = await GetOrdersById(id);
                if (result == null)
                {
                    _logger.LogError(ex, "Db Update Concurrency Exception occurred as Id is missing: {0}", id);
                    throw;
                }
                else
                {
                    _logger.LogError(ex, "An error occurred while processing the request.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }
    }
}
