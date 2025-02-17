using System.Data;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OrderManagement.Api.Features.Orders.Models;
using OrderManagement.Shared.Orders;

namespace OrderManagement.Api.Tests.Features.Orders;

public class OrderControllerTests
    : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;

    public OrderControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
            })
            .CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    [Fact]
    public async Task CreateOrder_WithValidRequest_ReturnsCreateOrder()
    {
        var request = new CreateOrderRequest
        {
            CustomerName = "Test Customer",
            Items =
            [
                new CreateOrderItemRequest
                {
                    ProductName = "Test Product",
                    Quantity = 1,
                    UnitPrice = 10.0m
                }
            ]
        };

        var response = await _client.PostAsJsonAsync("/api/orders", request);
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(order);
        Assert.Equal(request.CustomerName, order.CustomerName);
        Assert.Single(order.Items);
        Assert.Equal(10.0m, order.TotalAmount);
    }

    [Fact]
    public async Task GetOrder_WithExistingId_ReturnsOrder()
    {
        var createRequest = new CreateOrderRequest
        {
            CustomerName = "Test Customer",
            Items =
            [
                new CreateOrderItemRequest
                {
                    ProductName = "Test Product",
                    Quantity = 1,
                    UnitPrice = 10.0m
                }
            ]
        };

        var createResponse = await _client.PostAsJsonAsync("/api/orders", createRequest);
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        var response = await _client.GetAsync($"/api/orders/{createdOrder!.Id}");
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(order);
        Assert.Equal(createdOrder.Id, order.Id);
    }
}