using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.Commands;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.Handlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly OrderDbContext _db;

    public DeleteOrderHandler(OrderDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order is null)
            return false;

        _db.Orders.Remove(order);
        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}
    