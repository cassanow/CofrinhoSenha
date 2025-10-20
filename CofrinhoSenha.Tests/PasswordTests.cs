using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Service;
using Microsoft.EntityFrameworkCore;


namespace CofrinhoSenha.Tests;

public class PasswordTests
{
    [Fact]
    public void VerificaSeSenhaEstaCorretaDeveRetornarTrue()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Testdb")
            .Options;
        
        var context = new AppDbContext(options);
        var service = new PasswordService(context);

        var resultado = service.VerifyPassword("santosfc", "santosfc");
        
        Assert.True(resultado);
    }
}