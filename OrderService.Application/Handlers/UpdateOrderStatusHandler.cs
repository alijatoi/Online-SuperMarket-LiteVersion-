using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.Commands;
using OrderService.Domain.Entities;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.Handlers;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, Order>
{
    private readonly OrderDbContext _db;

    public UpdateOrderStatusHandler(OrderDbContext db)
    {
        _db = db;
    }

    public async Task<Order> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        // Find order
        var order = await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order is null)
            throw new KeyNotFoundException($"Order {request.OrderId} not found.");

        // Update status
        order.Status = request.NewStatus;
        await _db.SaveChangesAsync(cancellationToken);

        return order;
    }
}
