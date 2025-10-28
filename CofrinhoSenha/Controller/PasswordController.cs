using System.Security.Claims;
using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using CofrinhoSenha.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CofrinhoSenha.Controller;

[EnableRateLimiting("sliding")]
[Route("cofrinho/[controller]")]
[ApiController]
[Authorize]
public class PasswordController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IPasswordService _passwordService;
    private readonly AppDbContext _context;

    public PasswordController(IPasswordService passwordService,  AppDbContext context)
    {
        _passwordService = passwordService;
        _context = context;
    }
    
    [HttpPost("Generate")]
    public IActionResult Generate([FromBody] GeneratePasswordRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var cofrinho = _context.Cofrinho
            .FirstOrDefault(c => c.Id == request.CofrinhoId && c.UserId == userId);
        
        if (cofrinho == null)
            return BadRequest();
        
        var password = _passwordService.GenerateStrongPassword(request, cofrinho.Id);
        
        return Ok(new 
        { 
            senha = password.Nome,
            cofrinhoId = password.CofrinhoId,
            cofrinhoNome = cofrinho.Nome
        });
    }
}