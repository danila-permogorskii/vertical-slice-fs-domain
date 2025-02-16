module OrderManagement.Features.Tests.Orders.OrderTests

open OrderManagement.Features.Orders
open OrderManagement.Shared
open Xunit

module OrderTests =
    [<Fact>]
    let ``Create order should calculate correct total amount`` () =
        let customerName = "Test Customer"
        let items = [
            {ProductName = "Product 1"
             Quantity = 2
             UnitPrice = 10.0M
             LineTotal = 20.0M}
            {ProductName = "Product 2"
             Quantity = 2
             UnitPrice = 15.0M
             LineTotal = 15.0M}
        ]
        
        let order = Order.create customerName items
        
        Assert.Equal(35.0M, order.TotalAmount)
        Assert.Equal(OrderStatus.Created, order.Status)
        Assert.Equal(customerName, order.CustomerName)
        Assert.Equal(2, order.Items.Length)

