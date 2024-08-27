using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface IPlaylistTrackService
{
    Task AddTrackToPlaylistAsync(PlaylistTrackDto dto);
    Task RemoveTrackFromPlaylistAsync(PlaylistTrackDto dto);
}
