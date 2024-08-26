namespace Models.DTOs;

public class AlbumAddDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
}
