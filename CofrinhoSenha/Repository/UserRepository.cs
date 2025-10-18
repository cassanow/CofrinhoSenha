using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using CofrinhoSenha.Interface;
using Microsoft.EntityFrameworkCore;

namespace CofrinhoSenha.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetUserById(int id)
    {
        return await _context.User.Where(u => u.Id == id)
            .Include(u => u.Cofrinhos)
            .ThenInclude(c => c.Passwords)
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.User.Where(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.User.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> SaveUser(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser(User user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}