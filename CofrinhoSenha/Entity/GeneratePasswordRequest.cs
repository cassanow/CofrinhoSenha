using System.ComponentModel.DataAnnotations;

namespace CofrinhoSenha.Entity;

public class GeneratePasswordRequest
{
    
    [Range(1, 40, ErrorMessage = "A senha nao deve ser maior do que 40")]
    public int Length { get; set; } = 40;
    
    public int CofrinhoId { get; set; }
    public bool IncludeUppercase { get; set; } = true;
    
    public bool IncludeLowercase { get; set; } = true;
    
    public bool IncludeNumbers { get; set; } = true;
    
    public bool IncludeSpecialChars { get; set; } = true;
}