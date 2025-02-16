module OrderManagement.Features.Orders.Queries.GetOrder

open MediatR
open OrderManagement.Features.Orders
open OrderManagement.Features.Orders.Storage
open OrderManagement.Shared.Orders
open OrderManagement.Shared.Orders.Queries

type GetOrderHandler() =
    interface IRequestHandler<GetOrderQuery, OrderDto> with
        member this.Handle(query, cancellationToken) =
            task {
                let orderOption = Storage.getOrder query.OrderId
                return
                    match orderOption with
                    | Some order -> Order.toDto order
                    | None -> failwith "Order not found"
            }