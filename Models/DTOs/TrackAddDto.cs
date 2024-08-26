using Microsoft.AspNetCore.Http;

namespace Models.DTOs;

public class TrackAddDto
{
    public required string Name { get; set; }
    public required string Ganre { get; set; }
    public string? Description { get; set; }
    public int AlbumId { get; set; }
    public required IFormFile File { get; set; }
}
