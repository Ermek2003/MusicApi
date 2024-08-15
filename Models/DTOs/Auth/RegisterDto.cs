using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Auth;

public class RegisterDto
{
    public required string Username { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}
