using MediatR;

namespace OrderManagement.Shared.Orders.Commands;

public record CreateOrderCommand : IRequest<OrderDto>
{
    public string CustomerName { get; init; } = string.Empty;
    public List<CreateOrderItemCommand> Items { get; init; } = [];
}

public record CreateOrderItemCommand
{
    public string ProductName { get; init; } = String.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}