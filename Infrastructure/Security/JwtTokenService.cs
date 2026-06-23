

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App_QLPK.Application.Interfaces.Services;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App_QLPK.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption _option;
    
    public JwtTokenService(IOptions<JwtOption> option)
    {
        _option = option.Value;
    }

    public (string Token, DateTime ExpiresAt) GenerateToken(User user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_option.ExpireMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role?.Code ?? "User"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _option.Issuer,
            audience: _option.Audience,
            claims: claims,
            expires : expiresAt,
            signingCredentials: creds
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
    
}