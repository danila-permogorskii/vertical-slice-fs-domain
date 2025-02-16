module OrderManagement.Features.Orders.Storage

open System
open System.Collections.Concurrent

module Storage =
    let private orders = ConcurrentDictionary<Guid, Order>()
    
    let saveOrder (order: Order) =
        orders.TryAdd(order.Id, order) |> ignore
        order
        
    let getOrder (orderId: Guid) =
        match orders.TryGetValue orderId with
        | true, order -> Some order
        | _ -> None
