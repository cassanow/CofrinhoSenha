using CofrinhoSenha.Application.DTO;
using CofrinhoSenha.Domain.Entity;
using CofrinhoSenha.Domain.Interface;
using CofrinhoSenha.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CofrinhoSenha.API.Controller;

[Route("cofrinho/[controller]")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public UserController(IUserRepository userRepository,  IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }
    
    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] UserDTO dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Email = dto.Email,
            Username = dto.Username
        };
        
        user.Password = _passwordService.HashPassword(dto.Password);

        await _userRepository.SaveUser(user);
        
        return Ok(user);
    }

    [HttpPut("Update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDTO dto)
    {
        var user = await _userRepository.GetUserById(id);
        
        if (user == null)
            return NotFound();
        
        user.Email = dto.Email;
        user.Password = dto.Password;
        user.Username = dto.Username;

        await _userRepository.UpdateUser(user);
        
        return Ok(dto);
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepository.GetUserById(id);
        
        if (user == null)
            return NotFound();
        
        await _userRepository.DeleteUser(user);
        return Ok();
    }
}