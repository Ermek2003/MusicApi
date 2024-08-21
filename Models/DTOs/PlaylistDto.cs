namespace Models.DTOs;

public partial class PlaylistDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public ICollection<TrackDto>? Tracks { get; set; }
}
