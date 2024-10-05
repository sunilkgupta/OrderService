using Microsoft.AspNetCore.Mvc;
using Order.Business.Interfaces;
using Order.DataContext;

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
        private readonly IOrderBusiness _orderBusiness;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="orderBusiness"></param>
        public OrderController(ILogger<OrderController> logger, IOrderBusiness orderBusiness)
        {
            _logger = logger;
            _orderBusiness = orderBusiness;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Common.Entities.Order>>> Get()
        {            
            _logger.LogInformation("All Orders requested");
            var response = await _orderBusiness.GetOrders();
            if (!response.Any())
            {
                return NotFound();
            }
            return Ok(response);                     
        }

        /// <summary>
        /// Get specific order record
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Common.Entities.Order>> Get(Guid id)
        {            
            _logger.LogInformation("Order requested: Id is {0}", id);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Order requested: Id is null {0} ", id);
                return BadRequest();
            }
            else
            {
                var response = await _orderBusiness.GetOrdersById(id);
                if (response == null)
                {
                    _logger.LogWarning("Order requested: Id is {0}, does not exist.", id);
                    return NotFound();
                }               
                return Ok(response);
            }
        }

        /// <summary>
        /// Update exising order
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="order">order</param>
        /// <returns></returns>
        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Common.Entities.Order order)
        {
            _logger.LogInformation("Order updating: Id is {0}", id);
            
            if (id == Guid.Empty || order == null)
            {
                return BadRequest();
            }
            await _orderBusiness.UpdateOrder(id, order);
            return Ok(order);
            
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Common.Entities.Order>> Post(Common.Entities.Order order)
        {            
            _logger.LogInformation("New order creating..");
            if (order == null)
            {
                return BadRequest("Order is null");
            }            
            var response = await _orderBusiness.CreateOrder(order);
            if (response == null)
            {
                _logger.LogWarning("Order not created!");
                return NotFound();
            }
            return Created("", response);            
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
            _logger.LogInformation("Order deleting, Id is {0}", id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var response = await _orderBusiness.DeleteOrder(id);
            if (!response)
            {
                return NotFound();
            }
            return NoContent();                      
        }
    }
}
