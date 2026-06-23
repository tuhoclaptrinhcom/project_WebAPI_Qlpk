using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces.Services;

public interface IJwtTokenService
{
    /// <summary>
    ///     Tạo JWT cho user, trả về Token và thời điểm hết hạn.
    /// </summary>
    
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}
