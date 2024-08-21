namespace Models.DTOs;

public partial class PlaylistDto
{
    public class Edit
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}