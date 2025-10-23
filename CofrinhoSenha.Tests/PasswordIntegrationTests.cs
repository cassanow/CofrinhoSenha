using System.Net;
using System.Net.Http.Json;
using CofrinhoSenha.Data.Context;
using CofrinhoSenha.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace CofrinhoSenha.Tests;

public class PasswordIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public PasswordIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        
        _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
            });
        });
        
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("https://localhost"),
            HandleCookies = false,
            MaxAutomaticRedirections = 7
        });

        using var scope = _factory.Services.CreateScope();
        var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var context = dbFactory.CreateDbContext();
        context.Database.EnsureCreated();
    }


    [Fact]
    public async Task DeveRetornarOkSeRegisterBemSucedido()
    {
        var dto = new
        {
            Username = "arthur",
            Email = "arthur@gmail.com",
            Password = "santosfc"
        };
        
        var response = await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", dto);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
    }
}
