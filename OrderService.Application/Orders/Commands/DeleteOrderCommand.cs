using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace OrderService.Application.Orders.Commands
{


    // Command = delete an order
    public record DeleteOrderCommand(Guid OrderId) : IRequest<bool>;
}
