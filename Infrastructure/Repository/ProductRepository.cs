using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.IRepository;

namespace Infrastructure.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context; 
        }

        public Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
        {
           return _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        }
        public Task<List<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return _context.Products.AsNoTracking().ToListAsync(ct);
        }

        public async Task AddAsync(Product product, CancellationToken ct = default)
        {
            await _context.Products.AddAsync(product, ct);
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
        {
           return _context.SaveChangesAsync(ct);
        }

        public async Task<(List<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Products.AsNoTracking();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }


    }
}
