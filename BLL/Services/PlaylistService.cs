using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IGenericRepository<Playlist> _playlistRepository;
    public PlaylistService(IGenericRepository<Playlist> playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    public async Task<int> AddAsync(Playlist playlist)
    {
        await _playlistRepository.AddAsync(playlist);
        await _playlistRepository.SaveChangesAsync();
        return playlist.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var playlistId = await _playlistRepository.DeleteAsync(id);
        await _playlistRepository.SaveChangesAsync();
        return playlistId;
    }

    public async Task<List<Playlist>> GetAllAsync()
    {
        return await _playlistRepository.GetAll().ToListAsync();
    }

    public async Task<Playlist> GetByIdAsync(int id)
    {
        return await _playlistRepository.GetByIdAsync(id);
    }

    public async Task<int> Update(Playlist playlist)
    {
        var id = _playlistRepository.Update(playlist).Id;
        await _playlistRepository.SaveChangesAsync();
        return id;
    }
}
