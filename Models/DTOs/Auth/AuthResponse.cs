﻿namespace Models.DTOs.Auth;

public class AuthResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}
