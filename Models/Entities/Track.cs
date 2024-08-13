﻿namespace Models.Entities;

public class Track
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int PlaylistId { get; set; }
    public Playlist? Playlist { get; set; }
}
