namespace Models.Entities;

public class Playlist
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<PlaylistTrack>? PlaylistTracks { get; set; }
}
