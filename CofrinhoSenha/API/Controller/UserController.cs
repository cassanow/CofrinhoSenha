using CofrinhoSenha.Application.DTO;
using CofrinhoSenha.Domain.Entity;
using CofrinhoSenha.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CofrinhoSenha.API.Controller;

[Route("cofrinho/[controller]")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
            Password = dto.Password,
            Username = dto.Username
        };

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