using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Commands;

public record CreateOrderCommand(string CustomerId, List<OrderItem> Items) : IRequest<Order>;
