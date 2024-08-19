using Models.DTOs.Auth;

namespace Infrastructure.Interfaces.IServices;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterDto dto);
    Task<AuthResponse> LoginAsync(LoginDto dto);
    Task LogoutAsync(string userName);
    Task<AuthResponse> RefreshTokenAsync(string refreshToken);
}
