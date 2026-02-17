using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
    public interface IOrderService
    {
        public Task<OrderDetailsDto>CreateOrder(CreateOrderDto dto, CancellationToken ct = default);

        public Task<OrderDetailsDto?>GetOrderById(int id ,CancellationToken ct = default);
    }
}
