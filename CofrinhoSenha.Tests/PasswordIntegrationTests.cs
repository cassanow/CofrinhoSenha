using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace CofrinhoSenha.Tests;

public class PasswordIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;
    
    public PasswordIntegrationTests(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        
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
        var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var usuario = new User
        {
            Id = 1,
            Username = "admin",
            Password = "admin",
            Email = "admin@gmail.com",
        };
        context.User.Add(usuario);
        await context.SaveChangesAsync();

        var cofrinho = new Cofrinho
        {
            Nome = "CofrinhoSenha",
            UserId = usuario.Id
        };
        
        context.Cofrinho.Add(cofrinho);
        await context.SaveChangesAsync();
        
        var request = new GeneratePasswordRequest
        {
            CofrinhoId = cofrinho.Id
        };
        
        var key = "1ecio14h2vi08vh40hv840h848g98gbgh8v4984bhb3489h345b28953h24b8953bh8g4h89bbh89b53b5h8953b8h9b35h89b"u8.ToArray();
        var issuer = "cofrinho";
        var audience = "cofrinho";
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "arthur") }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        
        var response = await _client.PostAsJsonAsync("cofrinho/password/generate", request);
        
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Password>();
        
        Assert.NotNull(result);
        Assert.NotNull(result.Nome);
    }

}