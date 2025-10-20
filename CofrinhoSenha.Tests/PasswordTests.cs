using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Service;
using Microsoft.EntityFrameworkCore;


namespace CofrinhoSenha.Tests;

public class PasswordTests
{
    [Fact]
    public void DeveRetornarTrueSeASenhaEstiverCorreta()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;
        
        var context = new AppDbContext(options);
        var service = new PasswordService(context);

        var password = "santosfc";
        var hash = service.HashPassword(password);
        var resultado = service.VerifyPassword(hash, password);
        
        Assert.True(resultado);
    }

    [Fact]
    public void DeveMeRetornarUmPasswordHash()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;
        
        var context = new AppDbContext(options);
        var service = new PasswordService(context);

        var password = "santosfc";
        var hashPassword = service.HashPassword(password);

        Assert.NotEqual(hashPassword, password);
    }

    [Fact]
    public void DeveFalharSeAVerificacaoEstiverIncorreta()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;
        
        var context = new AppDbContext(options);
        var service = new PasswordService(context);
        
        var password = "santosfc";
        var hashPassword = service.HashPassword(password);
        
        Assert.False(service.VerifyPassword(hashPassword, "ferrari"));
    }

    [Fact]
    public void DeveFalharSeOHashForInvalido()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;
        
        var context = new AppDbContext(options);
        var service = new PasswordService(context);
        
        var hashInvalido = "hash-invalid-sei-la";
        
        var resultado = service.VerifyPassword(hashInvalido, "santosfc");
        
        Assert.False(resultado);
    }
}