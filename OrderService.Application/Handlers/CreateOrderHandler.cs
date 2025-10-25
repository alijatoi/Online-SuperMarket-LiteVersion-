using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderService.Application.Orders.Commands;
using OrderService.Domain.Entities;
using OrderService.Infrastructure;

namespace OrderService.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly OrderDbContext _db;

        public CreateOrderHandler(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var order = new Order
            {
                CustomerId = request.CustomerId,
                Items = request.Items,
                TotalPrice = request.Items.Sum(i => i.Price * i.Quantity),
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}
