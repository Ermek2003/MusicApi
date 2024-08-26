namespace Models.DTOs;

public class UserWithAlbumsDto
{
    public required string Name { get; set; }
    public ICollection<AlbumDto>? Albums { get; set; }
}
