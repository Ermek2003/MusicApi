using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface IAlbumService
{
    Task<int> AddAsync(AlbumDto.Add dto);
    Task<List<AlbumDto>> GetAllAsync();
}
