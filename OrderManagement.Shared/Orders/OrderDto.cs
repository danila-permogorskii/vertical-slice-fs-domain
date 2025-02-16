namespace OrderManagement.Shared.Orders;

public record OrderDto()
{
    public Guid Id { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public List<OrderItemDto> Items { get; init; } = [];
    public decimal TotalAmount { get; init; }
    public OrderStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record OrderItemDto
{
    public string ProductName { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal Features { get; init; }
}