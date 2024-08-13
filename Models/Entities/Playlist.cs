namespace Models.Entities;

public class Playlist
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Track>? Tracks { get; set; }
}
