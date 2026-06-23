
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);

    Task UpdateAsync(User user, CancellationToken ct = default);
}