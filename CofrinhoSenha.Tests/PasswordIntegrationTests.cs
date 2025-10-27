using System.Net;
using System.Net.Http.Json;
using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    public async Task DeveRetornarOkSeRegisterELoginBemSucedidoESeMeRetonarOTokenEUsername()
    {
        
        var registerDto = new User
        {
            Username = "arthur",
            Email = "arthur@gmail.com",
            Password = "santosfc1912!"
        };
        var responseRegister = await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", registerDto);
        
        var loginDto = new
        {
            Email = "arthur@gmail.com",
            Password = "santosfc1912!"
        };
        
        var responseLogin = await _client.PostAsJsonAsync("/cofrinho/Auth/Login", loginDto);
        
        Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);
        Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        
        var json =  await responseLogin.Content.ReadAsStringAsync();
        Assert.NotEmpty(json);
        Assert.Contains("token", json.ToLower());
        Assert.Contains("username", json.ToLower());
    }
    
    [Fact]
    public async Task DeveRetornarUnauthorizedSeEmailNaoForEncontrado()
    {
        
        var registerDto = new User
        {
            Username = "arthur",
            Email = "arthur@gmail.com",
            Password = "santosfc1912!"
        };
        await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", registerDto);
        
        var loginDto = new
        {
            Email = "ferrari@gmail.com",
            Password = "santosfc1912!"
        };
        
        var response = await _client.PostAsJsonAsync("/cofrinho/Auth/Login", loginDto);
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task DeveRetornarUnauthorizedSeASenhaEstiverDiferente()
    {
        
        var registerDto = new User
        {
            Username = "arthur",
            Email = "arthur@gmail.com",
            Password = "santosfc1912!"
        };
        await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", registerDto);
        
        var loginDto = new
        {
            Email = "arthur@gmail.com",
            Password = "ferrari2008"
        };
        
        var response = await _client.PostAsJsonAsync("/cofrinho/Auth/Login", loginDto);
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeveRetornarBadRequestSeEmailForInvalido()
    {
        var registerDto = new User
        {
            Username = "arthur",
            Email = "arthurgmail.com",
            Password = "santosfc1912!"
        };
        var responseRegister = await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", registerDto);
        
        var loginDto = new
        {
            Email = "ferrarigmail.com",
            Password = "santosfc1912!"
        };
        
        var response = await _client.PostAsJsonAsync("/cofrinho/Auth/Login", loginDto);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, responseRegister.StatusCode);
    }

    [Fact]
    public async Task DeveRetornarBadRequestSeASenhaForMenorQue10OuMaiorQue50()
    {
        var usuario1 = new User
        {
            Username = "arthur",
            Email = "arthur@gmail.com",
            Password = "santosfc"
        };
        
        var usuario2 = new User
        {
            Username = "arthur",
            Email = "arthur@gmail.com",
            Password = "123456890123456890123456890123456890123456898686868688668"
        };
        
        var response1 = await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", usuario1);
        var response2 = await _client.PostAsJsonAsync("/cofrinho/Auth/Register/", usuario2);
        
        
        Assert.Equal(HttpStatusCode.BadRequest, response1.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
    }
}
