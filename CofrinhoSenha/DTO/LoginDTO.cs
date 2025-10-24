using System.ComponentModel.DataAnnotations;

namespace CofrinhoSenha.DTO;

public class LoginDTO
{
    [EmailAddress]
    public string Email { get; set; }
    
    public string Password { get; set; }
}