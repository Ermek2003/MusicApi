using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface IPlaylistService
{
    Task<int> AddAsync(PlaylistAddDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(PlaylistEditDto dto);
    Task<List<PlaylistDto>> GetAllAsync();
    Task<PlaylistDto> GetByIdAsync(int id);
}
