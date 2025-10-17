namespace VerificacaoSenha.Domain.Entity;

public class ValidPasswordResponse
{
    public bool IsValid { get; set; }
    
    public string Strenght { get; set; }
    
    public int Score { get; set; }

    public List<string> Messages { get; set; } = new List<string>();
    
    public Dictionary<string, bool> Checks { get; set; } = new Dictionary<string, bool>()
    {
        { "MinLength", false },
        { "HasUpperCase", false },
        { "HasLowerCase", false },
        { "HasNumbers", false },
        { "HasSpecialChars", false },
    };
}