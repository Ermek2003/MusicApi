using Common.Resources;
using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services;

public class TrackService : ITrackService
{
    private readonly IGenericRepository<Track> _trackRepository;
    private readonly IGenericRepository<Album> _albumRepository;

    public TrackService(IGenericRepository<Track> trackRepository, IGenericRepository<Album> albumRepository)
    {
        _trackRepository = trackRepository;
        _albumRepository = albumRepository;
    }

    public async Task<int> AddAsync(TrackAddDto dto)
    {
        if (!await _albumRepository.AnyAsync(a => a.Id == dto.AlbumId))
            throw new InvalidOperationException(string.Format(ErrorMessages.AlbumNotFound, dto.AlbumId));

        if (await _trackRepository.AnyAsync(t => t.Name == dto.Name &&  t.AlbumId == dto.AlbumId))
            throw new InvalidOperationException(string.Format(ErrorMessages.TrackAlreadyExist, dto.Name));

        var track = dto.Adapt<Track>();

        track.Url = await SaveTrack(dto.File);

        await _trackRepository.AddAsync(track);
        await _trackRepository.SaveChangesAsync();

        return track.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var track = await _trackRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.TrackNotFound, id));

        if (!string.IsNullOrEmpty(track.Url) && File.Exists(track.Url))
            File.Delete(track.Url);

        _trackRepository.Delete(track);
        await _trackRepository.SaveChangesAsync();
    }

    public async Task<List<TrackDto>> GetAllAsync()
    {
        return await _trackRepository.GetAll().ProjectToType<TrackDto>().ToListAsync();
    }

    public async Task<TrackDto> GetByIdAsync(int id)
    {
        var track = await _trackRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.TrackNotFound));

        return track.Adapt<TrackDto>();
    }

    public async Task UpdateAsync(TrackEditDto dto)
    {
        var track = await _trackRepository.GetByIdAsync(dto.Id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.TrackNotFound, dto.Id));

        if (!string.IsNullOrEmpty(track.Url) && File.Exists(track.Url))
            File.Delete(track.Url);

        track = dto.Adapt(track);
        track.Url = await SaveTrack(dto.File);
        _trackRepository.Update(track);
        await _trackRepository.SaveChangesAsync();
    }

    private async Task<string> SaveTrack(IFormFile file)
    {
        // Убедитесь, что папка существует
        var folderPath = Path.Combine("Files", "music");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Укажите имя файла и путь
        var fileName = file.FileName;
        var filePath = Path.Combine(folderPath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return Path.Combine("Files", "music", fileName);
    }
}
