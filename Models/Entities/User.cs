namespace Models.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public string Role { get; set; } = "User";
    public IList<RefreshToken>? RefreshTokens { get; set; }
}
