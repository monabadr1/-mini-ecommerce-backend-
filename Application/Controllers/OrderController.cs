using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service) {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailsDto>> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
        { 
            return Ok(await _service.CreateOrder(dto, ct));

        } 

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDetailsDto>> Get(int id, CancellationToken ct)
        {
            var order = await _service.GetOrderById(id, ct);
            return order is null ? NotFound() : Ok(order);
        }
    }
}
