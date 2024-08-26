using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface ITrackService
{
    Task<int> AddAsync(TrackAddDto dto);
    Task<TrackDto> GetByIdAsync(int id);
    Task<List<TrackDto>> GetAllAsync();
    Task DeleteAsync(int id);
    Task UpdateAsync(TrackEditDto dto);
}
