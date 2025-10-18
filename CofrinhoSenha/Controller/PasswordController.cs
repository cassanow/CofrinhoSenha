using System.Security.Claims;
using CofrinhoSenha.Entity;
using CofrinhoSenha.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CofrinhoSenha.Controller;

[Route("cofrinho/[controller]")]
[ApiController]
[Authorize]
public class PasswordController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IPasswordService _passwordService;

    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }
    
    [HttpPost("Generate")]
    public IActionResult Generate([FromBody] GeneratePasswordRequest request)
    {
        var password = _passwordService.GenerateStrongPassword(request);
        
        return Ok(new { password });
    }
}