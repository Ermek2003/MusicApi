namespace Models.DTOs;

public partial class PlaylistDto
{
    public class Add
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
    }
}
