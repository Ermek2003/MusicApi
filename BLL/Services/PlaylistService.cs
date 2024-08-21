using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Mapster;

namespace BLL.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IGenericRepository<Playlist> _playlistRepository;
    private readonly IUserRepository _userRepository;
    public PlaylistService(IGenericRepository<Playlist> playlistRepository, IUserRepository userRepository)
    {
        _playlistRepository = playlistRepository;
        _userRepository = userRepository;
    }

    public async Task<int> AddAsync(PlaylistDto.Add dto)
    {
        if (await _userRepository.GetByIdAsync(dto.UserId) is null)
            throw new InvalidOperationException($"User with ID {dto.UserId} doesn't exist");

        var playlist = dto.Adapt<Playlist>();

        await _playlistRepository.AddAsync(playlist);
        await _playlistRepository.SaveChangesAsync();

        return playlist.Id;
    }

    public async Task DeleteAsync(int id)
    {
        if (await _playlistRepository.DeleteAsync(id) == 0)
            throw new InvalidOperationException($"Playlist with ID {id} doesn't exist");

        await _playlistRepository.SaveChangesAsync();
    }

    public async Task<List<PlaylistDto>> GetAllAsync()
    {
        return await _playlistRepository.GetAll().ProjectToType<PlaylistDto>().ToListAsync();
    }

    public async Task<PlaylistDto> GetByIdAsync(int id)
    {
        var playlist = await _playlistRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException($"Playlist with ID {id} doesn't exist");
        return playlist.Adapt<PlaylistDto>();
    }

    public async Task Update(PlaylistDto dto)
    {
        if (await _playlistRepository.GetByIdAsync(dto.Id) is null)
            throw new InvalidOperationException($"Playlist with ID {dto.Id} doesn't exist");
        _playlistRepository.Update(dto.Adapt<Playlist>());
        await _playlistRepository.SaveChangesAsync();
    }
}
