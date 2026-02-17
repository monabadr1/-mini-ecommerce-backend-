using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service) {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto, CancellationToken ct)
        {
            return Ok(await _service.CreateProduct(dto, ct));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(CancellationToken ct)
        { 
            return Ok(await _service.GetAllProduct(ct));
        }
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            return Ok(await _service.GetPagedProducts(page, pageSize, ct));
        }


    }
}
