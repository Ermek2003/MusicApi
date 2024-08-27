namespace Models.DTOs;

public class AlbumEditDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
