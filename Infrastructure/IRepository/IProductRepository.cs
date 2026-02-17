using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<Product>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Product product, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
        Task<(List<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

    }
}
