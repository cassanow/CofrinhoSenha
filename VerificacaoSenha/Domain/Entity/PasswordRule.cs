namespace VerificacaoSenha.Domain.Entity;

public class PasswordRule
{
    public int MinLength { get; set; } = 10;
    
    public bool RequireUppercase { get; set; } = true;
    
    public bool RequireLowercase { get; set; } = true;
    
    public bool RequireNumbers { get; set; } = true;
    
    public bool RequireSpecialChars { get; set; } = true;
    
    public bool IsActive { get; set; } = true;
}