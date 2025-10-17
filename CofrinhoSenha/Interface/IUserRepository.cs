using CofrinhoSenha.Domain.Entity;

namespace CofrinhoSenha.Domain.Interface;

public interface IUserRepository
{
    Task<User> GetUserById(int id);

    Task<User> GetByEmail(string email);
    Task<bool> Exists(int id);
    Task<bool> SaveUser(User user);
    
    Task<bool> UpdateUser(User user);
    
    Task<bool> DeleteUser(User user);
}