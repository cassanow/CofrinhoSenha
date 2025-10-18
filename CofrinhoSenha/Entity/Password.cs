using System.ComponentModel.DataAnnotations;

namespace CofrinhoSenha.Entity;

public class Password
{
    [Key]
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public int CofrinhoId { get; set; }
}