using System.Net.Http.Json;
using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace CofrinhoSenha.Tests;

public class PasswordIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public PasswordIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GerandoSenhaAtravesDoEndpoint()
    {
        var request = new GeneratePasswordRequest();

        var response = await _client.PostAsJsonAsync("cofrinho/password/generate", request);
        
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Password>();
        
        Assert.NotNull(result);
        Assert.NotNull(result.Nome);
    }

}