using CofrinhoSenha.Entity;

namespace CofrinhoSenha.Interface;

public interface IPasswordService
{
    string HashPassword(string password);
    
    bool VerifyPassword(string hashedPassword, string password);
   Password GenerateStrongPassword(GeneratePasswordRequest request, int cofrinhoId);
}