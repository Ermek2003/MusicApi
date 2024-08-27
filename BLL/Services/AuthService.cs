using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.Auth;
using Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Helpers;
using System.Security.Cryptography;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher _passwordHasher;
    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher();
    }

    public async Task<AuthResponse> RegisterAsync(RegisterDto dto)
    {
        if (await _userRepository.AnyAsync(u => u.Name == dto.Username))
            throw new Exception("Username already exist");
        var user = new User
        {
            Name = dto.Username,
            Email = dto.Email,
            Password = _passwordHasher.HashPassword(dto.Password)
        };

        var refreshToken = GenerateRefreshToken();
        user.RefreshTokens = new List<RefreshToken> { refreshToken };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse { RefreshToken = refreshToken.Token, Token = GenerateJwtToken(user) };
    }

    public async Task<AuthResponse> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByNameAsync(dto.Username);

        if (user is null || !_passwordHasher.VerifyPassword(dto.Password, user.Password))
            throw new Exception("Invalid username or password");

        if (user.RefreshTokens.Count >= 5)
            foreach(var refresh in user.RefreshTokens)
            {
                refresh.Revoked = DateTime.UtcNow;
            }

        var refreshToken = GenerateRefreshToken();
        user.RefreshTokens.Add(refreshToken);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse { RefreshToken = refreshToken.Token, Token = GenerateJwtToken(user) };
    }

    public async Task LogoutAsync(string userName)
    {
        var user = await _userRepository.GetByNameAsync(userName)
            ?? throw new Exception("User not found");
        foreach(var refreshToken in user.RefreshTokens)
        {
            refreshToken.Revoked = DateTime.UtcNow;
        }
        await _userRepository.SaveChangesAsync();
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken)
            ?? throw new Exception("Invalid refresh token");

        var existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);

        if (existingToken is null || !existingToken.IsActive)
            throw new Exception("Invalid refresh token");

        var newJwtToken = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();
        existingToken.Revoked = DateTime.UtcNow;
        user.RefreshTokens.Add(newRefreshToken);

        await _userRepository.SaveChangesAsync();

        return new AuthResponse
        {
            Token = newJwtToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    private RefreshToken GenerateRefreshToken()
    {
        using(var rng = RandomNumberGenerator.Create())
        {
            var randpmBytes = new byte[64];
            rng.GetBytes(randpmBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randpmBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            issuer: _configuration["Jwt:Issuer"],
            claims: claims,
            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

