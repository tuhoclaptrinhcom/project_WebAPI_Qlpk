
using System.ComponentModel.DataAnnotations;

namespace App_QLPK.Application.DTO.Auth;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}