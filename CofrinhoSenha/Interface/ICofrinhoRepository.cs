using CofrinhoSenha.Entity;

namespace CofrinhoSenha.Interface;

public interface ICofrinhoRepository
{
    Task<Cofrinho> GetById(int id);
    
    Task<IEnumerable<Cofrinho>> GetAll(int id);

    Task<bool> Create(Cofrinho cofrinho);
    
    Task<bool> Update(Cofrinho cofrinho);
    
    Task<bool> Delete(Cofrinho cofrinho);
}