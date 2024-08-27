using Common.Resources;
using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Mapster;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services;

public class PlaylistTrackService : IPlaylistTrackService
{
    private readonly IGenericRepository<PlaylistTrack> _playlistTrackRepository;
    private readonly IGenericRepository<Track> _trackRepository;
    private readonly IGenericRepository<Playlist> _playlistRepository;

    public PlaylistTrackService(IGenericRepository<PlaylistTrack> playlistTrackRepository,
        IGenericRepository<Track> trackRepository, IGenericRepository<Playlist> playlistRepository)
    {
        _playlistTrackRepository = playlistTrackRepository;
        _trackRepository = trackRepository;
        _playlistRepository = playlistRepository;
    }

    public async Task AddTrackToPlaylistAsync(PlaylistTrackDto dto)
    {
        if (!await _playlistRepository.AnyAsync(p => p.Id == dto.PlaylistId))
            throw new InvalidOperationException(string.Format(ErrorMessages.PlaylistNotFound, dto.PlaylistId));

        if (!await _trackRepository.AnyAsync(t => t.Id == dto.TrackId))
            throw new InvalidOperationException(string.Format(ErrorMessages.TrackNotFound, dto.TrackId));

        if (await _playlistTrackRepository.AnyAsync(pt => pt.TrackId == dto.TrackId && pt.PlaylistId == dto.PlaylistId))
            throw new InvalidOperationException(string.Format(ErrorMessages.TrackAlreadyInPlaylist, dto.TrackId, dto.PlaylistId));

        await _playlistTrackRepository.AddAsync(dto.Adapt<PlaylistTrack>());
        await _playlistTrackRepository.SaveChangesAsync();
    }

    public async Task RemoveTrackFromPlaylistAsync(PlaylistTrackDto dto)
    {
        var playlistTrack = await _playlistTrackRepository.FindAsync
            (pt => pt.PlaylistId == dto.PlaylistId && pt.TrackId == dto.TrackId)
            ?? throw new InvalidOperationException(string.Format
                (ErrorMessages.TrackNotInPlaylist, dto.TrackId, dto.PlaylistId));

        _playlistTrackRepository.Delete(playlistTrack);
        await _playlistTrackRepository.SaveChangesAsync();
    }
}
