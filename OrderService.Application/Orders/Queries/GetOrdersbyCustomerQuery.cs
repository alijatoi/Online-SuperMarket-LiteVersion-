using MediatR;
using OrderService.Application.Orders.Queries;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Orders.Queries
{
  public record GetOrdersByCustomerQuery(string CustomerId) : IRequest<List<Order>>;
}
