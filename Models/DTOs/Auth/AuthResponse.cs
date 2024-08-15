namespace Models.DTOs.Auth;

public class AuthResponse
{
    public bool IsSuccess { get; set; }
    public string? Token { get; set; }
    public List<string>? Errors { get; set; }
}
