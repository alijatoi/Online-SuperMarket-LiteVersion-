using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.Queries;
using OrderService.Domain.Entities;
using OrderService.Infrastructure;

namespace OrderService.Application.Handlers
{
    public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<Order>?>
    {
        private readonly OrderDbContext _db;

        public GetOrdersByCustomerQueryHandler(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<List<Order>?> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _db.Orders
                .Where(o => o.CustomerId == request.CustomerId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);

        }
    }
}

