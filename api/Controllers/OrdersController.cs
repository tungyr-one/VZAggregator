using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

            ///<summary>
            /// Gets order from service, if null returns NotFound
            ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            return await _ordersService.GetOrderAsync(id) switch
            {
                null => NotFound(),
                var order => Ok(order)
            };
        }

        ///<summary>
        /// Gets orders list
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<OrderDto[]>> GetOrders()
        {
            var orders = await _ordersService.GetOrdersAsync();
            HttpContext.Response.StatusCode = 200;
            await HttpContext.Response.WriteAsJsonAsync(orders);
            return new EmptyResult();
        }

        ///<summary>
        /// Creates order 
        ///</summary>
        [HttpPost("new")]
        public async Task<ActionResult> CreateOrderAsync(OrderDto newOrder)
        {
            if (await _ordersService.CreateAsync(newOrder)) return Ok();
            return BadRequest("Failed to create order");
        }

            ///<summary>
            /// Updates order 
            ///</summary>
        [HttpPut("{id}")]
        public Task Update(int id, OrderDto orderUpdate) => _ordersService.UpdateAsync(id, orderUpdate);

        ///<summary>
        /// Deletes order
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderAsync(int id)
        {
            if (await _ordersService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete order");
        }
    }
}