using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface IAlbumService
{
    Task<int> AddAsync(AlbumAddDto dto);
    Task<AlbumDto> GetByIdAsync(int id);
    Task<List<AlbumDto>> GetAllAsync();
    Task DeleteAsync(int id);
    Task UpdateAsync(AlbumDto dto);
}
