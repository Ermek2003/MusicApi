namespace Models.DTOs;

public partial class PlaylistAddDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
}
