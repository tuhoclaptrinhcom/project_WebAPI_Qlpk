
using App_QLPK.Application.DTO.Auth;

namespace App_QLPK.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);   
}