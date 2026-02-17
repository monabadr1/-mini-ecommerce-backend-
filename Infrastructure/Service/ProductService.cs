using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Shared;

namespace Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<ProductDto> CreateProduct(CreateProductDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required");

            if (dto.Price <= 0)
                throw new ArgumentException("Price must be above 0");

            if (dto.AvailableQuantity < 0)
                throw new ArgumentException("AvailableQuantity must be >= 0");

            var product = new Product
            {
                Name = dto.Name.Trim(),
                Price = dto.Price,
                AvailableQuantity = dto.AvailableQuantity
            };

            await _productRepo.AddAsync(product, ct);
            await _productRepo.SaveChangesAsync(ct);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                AvailableQuatity = product.AvailableQuantity
            };
        }

        public async Task<List<ProductDto>> GetAllProduct(CancellationToken ct = default)
        {
            var listProducts = await _productRepo.GetAllAsync(ct);

            return listProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                AvailableQuatity = p.AvailableQuantity
            }).ToList();
        }

        public async Task<PagedResult<ProductDto>> GetPagedProducts(int page, int pageSize, CancellationToken ct = default)
        {
            var (items, total) = await _productRepo.GetPagedAsync(page, pageSize, ct);

            return new PagedResult<ProductDto>
            {
                Page = page <= 0 ? 1 : page,
                PageSize = pageSize <= 0 ? 10 : pageSize,
                TotalCount = total,
                Items = items.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    AvailableQuatity = p.AvailableQuantity
                }).ToList()
            };
        }

    }
}
