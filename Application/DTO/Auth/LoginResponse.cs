
namespace App_QLPK.Application.DTO.Auth;
public class LoginResponse
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set;}
    public UserInfo User { get; set; } = null!;

}

public class UserInfo
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
}