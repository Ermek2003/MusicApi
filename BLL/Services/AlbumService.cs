using Common.Resources;
using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services;

public class AlbumService : IAlbumService
{
    private readonly IGenericRepository<Album> _albumRepository;
    private readonly IUserRepository _userRepository;
    public AlbumService(IGenericRepository<Album> albumRepository, IUserRepository userRepository)
    {
        _albumRepository = albumRepository;
        _userRepository = userRepository;
    }

    public async Task<int> AddAsync(AlbumAddDto dto)
    {
        if (await _userRepository.GetByIdAsync(dto.UserId) is null)
            throw new InvalidOperationException(string.Format(ErrorMessages.UserNotFound, dto.UserId));

        if (await _albumRepository.AnyAsync(a => a.Name == dto.Name && a.UserId == dto.UserId))
            throw new InvalidOperationException(string.Format(ErrorMessages.AlbumAlreadyExist, dto.Name));

        var album = dto.Adapt<Album>();

        await _albumRepository.AddAsync(album);
        await _albumRepository.SaveChangesAsync();
        return album.Id;
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _albumRepository.AnyAsync(a => a.Id == id))
            throw new InvalidOperationException(string.Format(ErrorMessages.AlbumNotFound, id));

        await _albumRepository.DeleteAsync(id);
        await _albumRepository.SaveChangesAsync();
    }

    public async Task<List<AlbumDto>> GetAllAsync()
    {
        return await _albumRepository.GetAll().ProjectToType<AlbumDto>().ToListAsync();
    }

    public async Task<AlbumDto> GetByIdAsync(int id)
    {
        var album = await _albumRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.AlbumNotFound, id));

        return album.Adapt<AlbumDto>();
    }

    public async Task UpdateAsync(AlbumDto dto)
    {
        var album = await _albumRepository.GetByIdAsync(dto.Id)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.AlbumNotFound, dto.Id));

        album = dto.Adapt(album);

        _albumRepository.Update(album);
        await _albumRepository.SaveChangesAsync();
    }
}
