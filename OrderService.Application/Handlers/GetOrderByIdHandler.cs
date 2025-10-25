using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.Queries;
using OrderService.Domain.Entities;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly OrderDbContext _db;

    public GetOrderByIdHandler(OrderDbContext db)
    {
        _db = db;
    }

    public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
    }
}
