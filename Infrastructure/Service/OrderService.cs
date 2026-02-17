using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class OrderService:IOrderService
    {
        private readonly IProductRepository _products;
        private readonly IOrderRepository _orders;

        public OrderService(IProductRepository products, IOrderRepository orders)
        {
            _products = products;
            _orders = orders;
        }

        public async Task<OrderDetailsDto> CreateOrder(CreateOrderDto dto, CancellationToken ct = default)
        {
            if (dto.Items == null || dto.Items.Count == 0)
                throw new ArgumentException("Order must have at least one item.");

            var order = new Order
            {
                CustomerName = dto.CustomerName.Trim(),
                CustomerEmail = dto.CustomerEmail.Trim(),
            };

            int totalItemsCount = 0;
            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                if (item.Quantity <= 0)
                    throw new ArgumentException("Quantity must be > 0.");

                var product = await _products.GetByIdAsync(item.ProductId, ct)
                    ?? throw new KeyNotFoundException($"Product {item.ProductId} not found.");

                if (product.AvailableQuantity < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for product {product.Id}.");

                product.AvailableQuantity -= item.Quantity;

                totalItemsCount += item.Quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                totalAmount += product.Price * item.Quantity;
            }

            var discount = CalculateDiscount(totalItemsCount); // per task 
            var finalAmount = totalAmount - (totalAmount * discount / 100m);

            order.TotalAmount = totalAmount;
            order.DiscountPercentage = discount;
            order.FinalAmount = finalAmount;

            await _products.SaveChangesAsync(ct);

            await _orders.AddAsync(order, ct);
            await _orders.SaveChangesAsync(ct);

            var saved = await _orders.GetByIdWithItemsAsync(order.Id, ct)
                ?? throw new Exception("Order saved but could not be loaded.");

            return MapToDetailsDto(saved);
        }

        public async Task<OrderDetailsDto?> GetOrderById(int id, CancellationToken ct = default)
        {
            var order = await _orders.GetByIdWithItemsAsync(id, ct);
            return order is null ? null : MapToDetailsDto(order);
        }

        private decimal CalculateDiscount(int totalItems)
            => totalItems >= 5 ? 10m : totalItems >= 2 ? 5m : 0m;

        private static OrderDetailsDto MapToDetailsDto(Order order)
        {
            var dto = new OrderDetailsDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                DiscountPercentage = order.DiscountPercentage,
                FinalAmount = order.FinalAmount,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.UnitPrice * i.Quantity
                }).ToList()
            };

            return dto;
        }

    }
}
