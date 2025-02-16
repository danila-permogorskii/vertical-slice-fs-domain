namespace OrderManagement.Features.Orders

open System
open System.Collections.Generic
open OrderManagement.Shared
open OrderManagement.Shared.Orders

type OrderItem = {
    ProductName: string
    Quantity: int
    UnitPrice: decimal
    LineTotal: decimal
}

type Order = {
    Id: Guid
    CustomerName: string
    Items: OrderItem list
    TotalAmount: decimal
    Status: OrderStatus
    CreatedAt: DateTime
}

module Order =
    let create customerName items =
        let calculateLineTotal item =
            decimal(item.Quantity) * item.UnitPrice
        
        let orderItems =
            items
            |> List.map (fun item ->
                {
                    ProductName = item.ProductName
                    Quantity = item.Quantity
                    UnitPrice = item.UnitPrice
                    LineTotal = item.LineTotal
                })
            
        let totalAmount =
            orderItems
            |> List.sumBy(fun item -> item.LineTotal)
            
        {
            Id = Guid.NewGuid()
            CustomerName = customerName
            Items = orderItems
            TotalAmount = totalAmount
            Status = OrderStatus.Created
            CreatedAt = DateTime.UtcNow
        }
    
    let toDto (order: Order) =
        OrderDto(
            Id = order.Id,
            CustomerName = order.CustomerName,
            Items = (order.Items
                |> List.map (fun item ->
                    OrderItemDto(
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice))
                |> List.toSeq
                |> List),
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt
        )