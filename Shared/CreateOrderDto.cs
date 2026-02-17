using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; } = default!;
        public string CustomerEmail { get; set; } = default!;
        public List<CreateOrderItemDto> Items { get; set; } = new();

    }
}
