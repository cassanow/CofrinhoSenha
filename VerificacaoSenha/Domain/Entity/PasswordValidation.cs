using System.ComponentModel.DataAnnotations;

namespace VerificacaoSenha.Domain.Entity;

public class PasswordValidation
{
    [Key]
    public int Id { get; set; }
    
    public string Password { get; set; }
    
    public int UserId { get; set; }
    
    public bool IsValid { get; set; }
    
    public int Score { get; set; }
    
    public string Strength { get; set; }
    
    public bool HasMinLength { get; set; }
    
    public bool HasUpperCase { get; set; }
    
    public bool HasLowerCase { get; set; }
    
    public bool HasNumbers { get; set; }
    
    public bool HasSpecialChars { get; set; }
}