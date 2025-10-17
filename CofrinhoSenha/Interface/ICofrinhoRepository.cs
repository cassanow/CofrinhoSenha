using CofrinhoSenha.Application.DTO;
using CofrinhoSenha.Domain.Entity;

namespace CofrinhoSenha.Interface;

public interface ICofrinhoRepository
{
    Task<Cofrinho> GetById(int id);
    
    Task<IEnumerable<Cofrinho>> GetAll();

    Task<bool> Create(Cofrinho cofrinho);
    
    Task<bool> Update(Cofrinho cofrinho);
    
    Task<bool> Delete(Cofrinho cofrinho);
}