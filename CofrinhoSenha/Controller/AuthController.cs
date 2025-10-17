using CofrinhoSenha.Application.DTO;
using CofrinhoSenha.Domain.Entity;
using CofrinhoSenha.Domain.Interface;
using CofrinhoSenha.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CofrinhoSenha.API.Controller;

[Route("cofrinho/[controller]")]
[ApiController]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService  _passwordService;

    public AuthController(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var user = await _userRepository.GetByEmail(dto.Email);
        
        if (user == null)
            return NotFound();
        
        var valid = _passwordService.VerifyPassword(dto.Password,  user.Password);
        
        if (!valid)
            return Unauthorized();
        
        if(user.Password != dto.Password && user.Email != dto.Email)
            return Unauthorized();
        
        return Ok();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserDTO dto)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        var response = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = _passwordService.HashPassword(dto.Password)
        };

        await _userRepository.SaveUser(response);
        
        return Ok(response);
    }
}