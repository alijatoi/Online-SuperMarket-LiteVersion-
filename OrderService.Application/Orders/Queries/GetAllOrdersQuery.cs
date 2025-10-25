using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries;

// Query = request for all orders
public record GetAllOrdersQuery() : IRequest<List<Order>>;