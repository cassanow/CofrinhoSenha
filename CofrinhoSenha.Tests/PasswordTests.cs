using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
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

    [Fact]
    public void DeveMeRetornarUmaSenhaDoTamanhoPadrao40()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;

        var context = new AppDbContext(options);
        var service = new PasswordService(context);
        
        var request = new GeneratePasswordRequest();
        var cofrinho = new Cofrinho();

        var resultado = service.GenerateStrongPassword(request, cofrinho.Id);
        
        Assert.Equal(40, resultado.Nome.Length);
    }
    
    [Fact]
    public void DeveMeRetornarUmaSenhaDeTamanhoEscolhidoPeloUsuario()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;

        var context = new AppDbContext(options);
        var service = new PasswordService(context);
        
        var request = new GeneratePasswordRequest();
        var cofrinho = new Cofrinho();
        request.Length = 10;

        var resultado = service.GenerateStrongPassword(request, cofrinho.Id);
        
        Assert.Equal(10, resultado.Nome.Length);
    }
    
    [Fact]
    public void DeveFalharSeASenhaForMaiorQue40()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;
        
        var context = new AppDbContext(options);
        var service = new PasswordService(context);
        
        var request = new GeneratePasswordRequest();
        var cofrinho = new Cofrinho();
        request.Length = 41;
        
        var resultado = service.GenerateStrongPassword(request, cofrinho.Id);
        
        Assert.NotNull(resultado);
        Assert.NotNull(resultado.Nome);
        Assert.False(resultado.Nome.Length <= 40);
    }

    [Fact]
    public void DeveMeRetornarSenhasDiferentes()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;

        var context = new AppDbContext(options);
        var service = new PasswordService(context);
        
        var request = new GeneratePasswordRequest();
        var cofrinho = new Cofrinho();

        var senha1 = service.GenerateStrongPassword(request, cofrinho.Id);
        var senha2 = service.GenerateStrongPassword(request, cofrinho.Id);
        
        Assert.NotEqual(senha1, senha2);
    }
}