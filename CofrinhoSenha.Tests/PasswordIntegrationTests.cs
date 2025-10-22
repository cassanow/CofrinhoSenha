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

}