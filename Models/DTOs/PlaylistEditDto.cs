namespace Models.DTOs;

public partial class PlaylistEditDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}