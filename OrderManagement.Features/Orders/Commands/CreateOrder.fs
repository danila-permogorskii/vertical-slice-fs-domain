module OrderManagement.Features.Orders.Commands.CreateOrder

open MediatR
open OrderManagement.Features.Orders.Storage
open OrderManagement.Shared.Orders
open OrderManagement.Shared.Orders.Commands
open System.Threading.Tasks
open OrderManagement.Features.Orders

type CreateOrderHandler() =
    interface IRequestHandler<CreateOrderCommand, OrderDto> with
        member this.Handle(command, cancellationToken) =
            task {
                let items =
                    command.Items
                    |> Seq.map (fun item -> {
                        ProductName = item.ProductName
                        Quantity = item.Quantity
                        UnitPrice = item.UnitPrice
                        LineTotal = decimal(item.Quantity) * item.UnitPrice
                    })
                    |> Seq.toList
                
                let order =
                    Order.create command.CustomerName items
                    |> Storage.saveOrder
                
                return Order.toDto order
            }