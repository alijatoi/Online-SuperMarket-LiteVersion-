using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries;

// Query = request for order by ID
public record GetOrderByIdQuery(Guid OrderId) : IRequest<Order?>;
