using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Auth;

namespace WebApi.Controllers;

/// <summary>
/// Controller for work with authentication
/// </summary>
[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Reagister(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (result.IsSuccess)
            return Ok(result);

        return Unauthorized(result);
    }

    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return Ok(new { Message = "Logged out successfully." });
    }
}
