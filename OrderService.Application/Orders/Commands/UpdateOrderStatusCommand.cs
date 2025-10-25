using MediatR;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Orders.Commands
{

    // Command = request to change order status
    public record UpdateOrderStatusCommand(Guid OrderId, string NewStatus) : IRequest<Order>;
}
