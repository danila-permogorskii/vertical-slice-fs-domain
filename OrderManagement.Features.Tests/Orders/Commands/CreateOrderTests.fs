module OrderManagement.Features.Tests.Orders.Commands.CreateOrderTests

open System.Threading
open MediatR
open OrderManagement.Features.Orders.Commands.CreateOrder
open OrderManagement.Shared.Orders
open OrderManagement.Shared.Orders.Commands
open Xunit

module CreateOrderTests =
    [<Fact>]
    let ``Handler should create order and return DTO`` () =
        task {
            let handler = CreateOrderHandler()
            let command = CreateOrderCommand(
                CustomerName = "Test Customer",
                Items = System.Collections.Generic.List<CreateOrderItemCommand>(
                    [
                        CreateOrderItemCommand(
                            ProductName = "TestProduct",
                            Quantity = 1,
                            UnitPrice = 10.0M)
                    ] |> List.toSeq))
            
            let! result = (handler :> IRequestHandler<CreateOrderCommand, OrderDto>)
                              .Handle(command, CancellationToken.None)
            
            Assert.NotNull(result)
            Assert.Equal(command.CustomerName, result.CustomerName)
            Assert.Equal(1, result.Items.Count)
            Assert.Equal(10.0M, result.TotalAmount)
        }