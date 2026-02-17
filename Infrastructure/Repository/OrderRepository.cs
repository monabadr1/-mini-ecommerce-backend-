using Domain.Entities;
using Infrastructure.IRepository;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) {
            _context = context;
        }

        public async Task AddAsync(Order order, CancellationToken ct = default)
        {
            await _context.Orders.AddAsync(order, ct);
        }

        public Task<Order?> GetByIdWithItemsAsync(int id, CancellationToken ct = default)
        {
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id, ct);
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
        {
           return _context.SaveChangesAsync(ct);
        }

    }
}
