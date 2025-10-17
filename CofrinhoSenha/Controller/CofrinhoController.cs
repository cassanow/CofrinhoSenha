using System.Security.Claims;
using CofrinhoSenha.DTO;
using CofrinhoSenha.Entity;
using CofrinhoSenha.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CofrinhoSenha.Controller;

[Route("cofrinho/[controller]")]
[ApiController]
public class CofrinhoController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ICofrinhoRepository _cofrinhoRepository;

    public CofrinhoController(ICofrinhoRepository cofrinhoRepository)
    {
        _cofrinhoRepository = cofrinhoRepository;
    }

   [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var cofrinhos = await _cofrinhoRepository.GetAll(userId);
        
        if(!cofrinhos.Any())
            return NotFound();
        
        return Ok(cofrinhos);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var cofrinho = await _cofrinhoRepository.GetById(id);
        
        if(cofrinho.UserId != userId)
            return Unauthorized();
        
        return Ok(cofrinho);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(CofrinhoDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = new Cofrinho
        {
            Nome = dto.Nome,
            UserId = userId,
        };
        
        if(response.UserId != userId)
            return Unauthorized();

        await _cofrinhoRepository.Create(response);
        
        return Ok(response);
    }


    [HttpPut("Update/{id:int}")]
    public async Task<IActionResult> Update(int id, CofrinhoDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var cofrinho = await _cofrinhoRepository.GetById(id);
        
        if(cofrinho == null)
            return NotFound();
        
        if(cofrinho.UserId != userId)
            return Unauthorized();
        
        cofrinho.Nome = dto.Nome;
        cofrinho.UserId = userId;
        
        await _cofrinhoRepository.Update(cofrinho);
        
        return Ok(dto);
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var cofrinho = await _cofrinhoRepository.GetById(id);
        
        if(cofrinho == null)
            return NotFound();
        
        if(cofrinho.UserId != userId)
            return Unauthorized();
        
        await _cofrinhoRepository.Delete(cofrinho);
        
        return Ok();
    }
}