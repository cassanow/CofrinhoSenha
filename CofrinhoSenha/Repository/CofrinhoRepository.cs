using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using CofrinhoSenha.Interface;
using Microsoft.EntityFrameworkCore;

namespace CofrinhoSenha.Repository;

public class CofrinhoRepository : ICofrinhoRepository
{
    private readonly AppDbContext _context;

    public CofrinhoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Cofrinho> GetById(int id)
    {
        return await _context.Cofrinho.Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Cofrinho>> GetAll(int id)
    {
        return await _context.Cofrinho.Where(c => c.UserId == id).ToListAsync();
    }

    public async Task<bool> Create(Cofrinho cofrinho)
    {
        _context.Cofrinho.Add(cofrinho);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Cofrinho cofrinho)
    {
        _context.Entry(cofrinho).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Cofrinho cofrinho)
    {
        _context.Cofrinho.Remove(cofrinho);
        await _context.SaveChangesAsync();
        return true;
    }
}