namespace Models.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public string Role { get; set; } = "User";

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    public ICollection<Playlist>? Playlists { get; set; }
    public ICollection<Album>? Albums { get; set; }
}
