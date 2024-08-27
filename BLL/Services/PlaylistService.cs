using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Mapster;
using Common.Resources;

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

    public async Task<int> AddAsync(PlaylistAddDto dto)
    {
        if (await _userRepository.GetByIdAsync(dto.UserId) is null)
            throw new InvalidOperationException(string.Format(ErrorMessages.UserNotFound, dto.UserId));

        if (await _playlistRepository.AnyAsync(p => p.Name == dto.Name && p.UserId == dto.UserId))
            throw new InvalidOperationException(string.Format(ErrorMessages.PlaylistAlreadyExist, dto.UserId));

        var playlist = dto.Adapt<Playlist>();

        await _playlistRepository.AddAsync(playlist);
        await _playlistRepository.SaveChangesAsync();

        return playlist.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var playlist = await _playlistRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.PlaylistNotFound, id));

        _playlistRepository.Delete(playlist);

        await _playlistRepository.SaveChangesAsync();
    }

    public async Task<List<PlaylistDto>> GetAllAsync()
    {
        return await _playlistRepository.GetAll().ProjectToType<PlaylistDto>().ToListAsync();
    }

    public async Task<PlaylistDto> GetByIdAsync(int id)
    {
        var playlist = await _playlistRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.PlaylistNotFound, id));
        return playlist.Adapt<PlaylistDto>();
    }

    public async Task UpdateAsync(PlaylistEditDto dto)
    {
        if (await _playlistRepository.GetByIdAsync(dto.Id) is null)
            throw new InvalidOperationException(string.Format(ErrorMessages.PlaylistNotFound, dto.Id));
        _playlistRepository.Update(dto.Adapt<Playlist>());
        await _playlistRepository.SaveChangesAsync();
    }
}
