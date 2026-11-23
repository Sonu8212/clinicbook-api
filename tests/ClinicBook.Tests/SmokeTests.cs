using System.Net;
using System.Net.Http.Json;
using ClinicBook.Api.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ClinicBook.Tests;

/// <summary>
/// Spins up the real API in memory (no network, no ports) and calls it over HTTP.
/// </summary>
public sealed class SmokeTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Health_live_returns_200()
    {
        var response = await _client.GetAsync("/health/live");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task System_info_returns_api_details()
    {
        var info = await _client.GetFromJsonAsync<SystemInfoResponse>("/api/v1/system/info");

        Assert.NotNull(info);
        Assert.Equal("ClinicBook API", info.Name);
        Assert.Equal("v1", info.Version);
    }

    [Fact]
    public async Task OpenApi_document_is_available_in_development()
    {
        var response = await _client.GetAsync("/openapi/v1.json");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Unknown_route_returns_problem_details()
    {
        var response = await _client.GetAsync("/api/v1/does-not-exist");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
