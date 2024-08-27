namespace Models.Entities;

public class Track
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Ganre { get; set; }
    public string? Description { get; set; }
    public required string Url { get; set; }
    public int AlbumId { get; set; }
    public Album? Album { get; set; }
    public ICollection<PlaylistTrack>? PlaylistTracks { get; set; }
}
