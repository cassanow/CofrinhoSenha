using CofrinhoSenha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CofrinhoSenha.Infrastructure.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<User> User { get; set; }
}