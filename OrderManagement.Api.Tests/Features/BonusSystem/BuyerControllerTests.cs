using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace OrderManagement.Api.Tests.Features.BonusSystem;

/// <summary>
/// Test class demonstrating how to test a BonusSystem BuyerController
/// This is a template for the bonus-system project
/// </summary>
public class BuyerControllerTests
    : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;

    public BuyerControllerTests(WebApplicationFactory<Program> factory)
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
    public async Task GetBonusSummary_WithValidUserId_ReturnsBonusSummary()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/buyer/{userId}/bonus-summary");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // In a real test, we would also verify the response content:
        // var bonusSummary = await response.Content.ReadFromJsonAsync<BonusSummaryDto>();
        // Assert.NotNull(bonusSummary);
        // Assert.Equal(expectedBalance, bonusSummary.CurrentBalance);
    }

    [Fact]
    public async Task GetTransactionHistory_WithValidUserId_ReturnsTransactionHistory()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/buyer/{userId}/transactions");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // In a real test, we would also verify the response content:
        // var transactions = await response.Content.ReadFromJsonAsync<List<TransactionDto>>();
        // Assert.NotNull(transactions);
        // Assert.NotEmpty(transactions);
    }

    [Fact]
    public async Task CancelTransaction_WithValidTransaction_ReturnsSuccess()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        // Act
        var response = await _client.PostAsync($"/api/buyer/{userId}/transactions/{transactionId}/cancel", null);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // In a real test, we would also verify the response content:
        // var result = await response.Content.ReadFromJsonAsync<bool>();
        // Assert.True(result);
    }

    [Fact]
    public async Task FindStoresByCategory_WithValidCategory_ReturnsStores()
    {
        // Arrange
        var category = "Electronics";

        // Act
        var response = await _client.GetAsync($"/api/buyer/stores?category={category}");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // In a real test, we would also verify the response content:
        // var stores = await response.Content.ReadFromJsonAsync<List<StoreDto>>();
        // Assert.NotNull(stores);
        // Assert.NotEmpty(stores);
        // Assert.Contains(stores, s => s.Category == category);
    }
}