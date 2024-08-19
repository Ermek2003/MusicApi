using Models.Entities;

namespace Infrastructure.Interfaces.IServices;

public interface IPlaylistService
{
    Task<int> AddAsync(Playlist playlist);
    Task<int> DeleteAsync(int id);
    Task<int> Update(Playlist playlist);
    Task<List<Playlist>> GetAllAsync();
    Task<Playlist> GetByIdAsync(int id);
}
