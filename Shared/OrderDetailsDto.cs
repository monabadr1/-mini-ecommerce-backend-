using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public string CustomerEmail { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal FinalAmount { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
    }
}
