using Microsoft.EntityFrameworkCore;
using VerificacaoSenha.Domain.Entity;

namespace VerificacaoSenha.Infrastructure.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}   
    
    public DbSet<PasswordValidation> Password { get; set; }
    
}