﻿namespace Models.Entities;

public class Album
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Track>? Tracks { get; set; }
}
