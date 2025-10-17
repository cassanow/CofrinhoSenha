using System.ComponentModel.DataAnnotations;

namespace CofrinhoSenha.Entity;

public class Cofrinho
{
    [Key]
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public int UserId { get; set; }
}