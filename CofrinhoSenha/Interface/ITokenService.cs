using CofrinhoSenha.Domain.Entity;

namespace CofrinhoSenha.Interface;

public interface ITokenService
{
    string GenerateToken(User user);
}