

using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly QlpkDbContext _context;
    
    public UserRepository(QlpkDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        return _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username, ct);
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(ct);
    }
}