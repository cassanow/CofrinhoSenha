namespace CofrinhoSenha.Entity;

public class GeneratePasswordRequest
{
    public int Length { get; set; } = 20;
    
    public bool IncludeUppercase { get; set; } = true;
    
    public bool IncludeLowercase { get; set; } = true;
    
    public bool IncludeNumbers { get; set; } = true;
    
    public bool IncludeSpecialChars { get; set; } = true;
}