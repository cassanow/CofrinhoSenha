namespace CofrinhoSenha.Interface;

public interface IPasswordService
{
    string HashPassword(string password);
    
    bool VerifyPassword(string hashedPassword, string password);
}