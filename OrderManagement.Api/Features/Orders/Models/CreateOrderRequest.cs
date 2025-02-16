namespace OrderManagement.Api.Features.Orders.Models;

public record CreateOrderRequest
{
    public required string CustomerName { get; init; }
    public required List<CreateOrderItemRequest> Items { get; init; }
}

public record CreateOrderItemRequest
{
    public required string ProductName { get; init; }
    public required int Quantity { get; init; }
    public required decimal UnitPrice { get; init; }
}