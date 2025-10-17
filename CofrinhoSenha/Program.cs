using CofrinhoSenha.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using CofrinhoSenha.Infrastructure.Data.Context;
using CofrinhoSenha.Infrastructure.Data.Repository;
using CofrinhoSenha.Infrastructure.Interface;
using CofrinhoSenha.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
