using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Infrastructure.IService
{
    public interface IProductService
    {
        public Task<ProductDto> CreateProduct(CreateProductDto dto, CancellationToken ct = default);

        public Task<List<ProductDto>> GetAllProduct(CancellationToken ct = default);

        Task<PagedResult<ProductDto>> GetPagedProducts(int page, int pageSize, CancellationToken ct = default);

    }
}
