﻿using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace WebApi.Controllers;

/// <summary>
/// Controller for work with users
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("PromoteUserToAdmin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task PromoteUserToAdmin(int userId)
    {
        await _userService.AssignAdminRoleToUser(userId);
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }
}
