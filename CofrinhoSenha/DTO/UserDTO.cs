using System.ComponentModel.DataAnnotations;

namespace CofrinhoSenha.DTO;

public class UserDTO
{
    public string Username { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [MinLength(10), MaxLength(50)]
    public string Password { get; set; }
}