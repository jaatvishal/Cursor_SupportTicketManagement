using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupportTicket.Core.DTOs;
using SupportTicket.Core.Entities;
using SupportTicket.Core.Enums;
using SupportTicket.Infrastructure.Data;

namespace SupportTicket.Tests;

public class TicketStatusApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public TicketStatusApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _factory.SeedDatabase();
    }

    [Fact]
    public async Task ValidTransition_OpenToInProgress_ShouldSucceed()
    {
        var ticketId = await CreateTicketAsync();
        var response = await _client.PatchAsJsonAsync($"/api/tickets/{ticketId}/status",
            new UpdateStatusRequest(TicketStatus.InProgress));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var ticket = await response.Content.ReadFromJsonAsync<TicketDto>();
        Assert.Equal(TicketStatus.InProgress, ticket!.Status);
    }

    [Fact]
    public async Task ValidTransition_FullLifecycle_ShouldSucceed()
    {
        var ticketId = await CreateTicketAsync();

        await PatchStatus(ticketId, TicketStatus.InProgress);
        await PatchStatus(ticketId, TicketStatus.Resolved);
        var response = await PatchStatus(ticketId, TicketStatus.Closed);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var ticket = await response.Content.ReadFromJsonAsync<TicketDto>();
        Assert.Equal(TicketStatus.Closed, ticket!.Status);
    }

    [Fact]
    public async Task InvalidTransition_OpenToResolved_ShouldReturn422()
    {
        var ticketId = await CreateTicketAsync();
        var response = await _client.PatchAsJsonAsync($"/api/tickets/{ticketId}/status",
            new UpdateStatusRequest(TicketStatus.Resolved));

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        Assert.Contains("Invalid status transition", error!.Error);
    }

    [Fact]
    public async Task InvalidTransition_OpenToClosed_ShouldReturn422()
    {
        var ticketId = await CreateTicketAsync();
        var response = await _client.PatchAsJsonAsync($"/api/tickets/{ticketId}/status",
            new UpdateStatusRequest(TicketStatus.Closed));

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async Task InvalidTransition_FromClosed_ShouldReturn422()
    {
        var ticketId = await CreateTicketAsync();
        await PatchStatus(ticketId, TicketStatus.InProgress);
        await PatchStatus(ticketId, TicketStatus.Resolved);
        await PatchStatus(ticketId, TicketStatus.Closed);

        var response = await _client.PatchAsJsonAsync($"/api/tickets/{ticketId}/status",
            new UpdateStatusRequest(TicketStatus.Open));

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async Task CreateTicket_WithoutRequiredFields_ShouldReturn400()
    {
        var response = await _client.PostAsJsonAsync("/api/tickets",
            new CreateTicketRequest("", "", TicketPriority.Medium, null, 0));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<int> CreateTicketAsync()
    {
        var response = await _client.PostAsJsonAsync("/api/tickets",
            new CreateTicketRequest("Test Ticket", "Test description", TicketPriority.Medium, null, 1));
        response.EnsureSuccessStatusCode();
        var ticket = await response.Content.ReadFromJsonAsync<TicketDto>();
        return ticket!.Id;
    }

    private async Task<HttpResponseMessage> PatchStatus(int ticketId, string status)
    {
        return await _client.PatchAsJsonAsync($"/api/tickets/{ticketId}/status",
            new UpdateStatusRequest(status));
    }
}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  private bool _seeded;

  protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
  {
    builder.UseEnvironment("Testing");
  }

  public void SeedDatabase()
  {
    if (_seeded) return;

    using var scope = Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
      db.Users.AddRange(
          new User { Id = 1, Name = "Alice Admin", Email = "alice@company.com", Role = "Admin" },
          new User { Id = 2, Name = "Bob Agent", Email = "bob@company.com", Role = "Agent" },
          new User { Id = 4, Name = "Dave User", Email = "dave@company.com", Role = "User" }
      );
      db.SaveChanges();
    }

    _seeded = true;
  }
}
