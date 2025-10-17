namespace CofrinhoSenha.Infrastructure.Interface;

public interface IPasswordService
{
    string HashPassword(string password);
}