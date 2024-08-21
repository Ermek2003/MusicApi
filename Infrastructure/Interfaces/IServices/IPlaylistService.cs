using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface IPlaylistService
{
    Task<int> AddAsync(PlaylistDto.Add dto);
    Task DeleteAsync(int id);
    Task Update(PlaylistDto dto);
    Task<List<PlaylistDto>> GetAllAsync();
    Task<PlaylistDto> GetByIdAsync(int id);
}
