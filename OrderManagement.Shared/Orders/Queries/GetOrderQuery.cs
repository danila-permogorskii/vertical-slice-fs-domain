using MediatR;

namespace OrderManagement.Shared.Orders.Queries;

public record GetOrderQuery : IRequest<OrderDto?>
{
    public Guid OrderId { get; init; }
}