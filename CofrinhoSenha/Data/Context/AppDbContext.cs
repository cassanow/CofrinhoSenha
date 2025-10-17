using CofrinhoSenha.Entity;
using Microsoft.EntityFrameworkCore;

namespace CofrinhoSenha.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<User> User { get; set; }
    
    public DbSet<Cofrinho> Cofrinho{ get; set; }
}