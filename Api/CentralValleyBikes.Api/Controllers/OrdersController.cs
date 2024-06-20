using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CentralValleyBikes.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService<Order, int> _orderService;

        public OrdersController(IOrderService<Order, int> orderService/*, ILogger<OrdersController> logger*/)
        {
            //_logger = logger;
            _orderService = orderService;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllAsync();

            if (orders.Any())
            {
                return Ok(orders);
            }

            return NotFound();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrdersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Delete(int id)
        {
        }
    }
}
