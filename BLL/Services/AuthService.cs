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
        if (await _userRepository.UserExistAsync(dto.Username))
        {
            return new AuthResponse { IsSuccess = true, Errors = new List<string> { "Username already exist" } };
        }
        var user = new User
        {
            Name = dto.Username,
            Email = dto.Email,
            Password = _passwordHasher.HashPassword(dto.Password)
        };  

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse { IsSuccess = true, Token = GenerateJwtToken(user) };
    }

    public async Task<AuthResponse> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByNameAsync(dto.Username);

        if (user is null || !_passwordHasher.VerifyPassword(dto.Password, user.Password))
            return new AuthResponse 
            { 
                IsSuccess = false,
                Errors = new List<string> { "Invalid username or password" } 
            };

        return new AuthResponse { IsSuccess = true, Token = GenerateJwtToken(user) };
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
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
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

