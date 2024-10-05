using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;

namespace Order.API.Controllers
{
    /// <summary>
    /// Provides all orders related data  
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderAPIContext _context;

        public OrderController(ILogger<OrderController> logger, OrderAPIContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Common.Entities.Order>>> Get()
        {
            try
            {
                _logger.LogInformation("All Orders requested");
                if (_context.Order == null)
                {
                    return NotFound();
                }
                return await _context.Order.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }          
        }

        /// <summary>
        /// Get specific order record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Common.Entities.Order>> Get(Guid id)
        {
            try
            {
                _logger.LogInformation("Order requested: Id is {0}", id);
                if (_context.Order == null)
                {
                    return NotFound();
                }
                var order = await _context.Order.FindAsync(id);

                if (order == null)
                {
                    return NotFound();
                }
                return order;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }
        }

        /// <summary>
        /// Update exising order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Common.Entities.Order order)
        {
            _logger.LogInformation("Order updating: Id is {0}", id);
            try
            {
                if (id != order.ItemId)
                {
                    return BadRequest();
                }

                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!OrderExists(id))
                {
                    _logger.LogError(ex, "Db Update Concurrency Exception occurred as Id is missing: {0}", id);
                    return NotFound();
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
                return StatusCode(500, "An error occurred.");
            }
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Common.Entities.Order>> Post(Common.Entities.Order order)
        {
            try
            {
                _logger.LogInformation("New order creating..");
                if (_context.Order == null)
                {
                    return Problem("Entity set 'OrderAPIContext.Order'  is null.");
                }
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = order.OrderId }, order);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }
          
        }

        /// <summary>
        /// Delete existing order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation("Order deleting, Id is {0}", id);
                if (_context.Order == null)
                {
                    return NotFound();
                }
                var order = await _context.Order.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order deleted successfully, Id is {0}", id);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }            
        }

        private bool OrderExists(Guid id)
        {
            return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
