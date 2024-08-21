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

    public async Task<int> AddAsync(AlbumDto.Add dto)
    {
        if (await _userRepository.GetByIdAsync(dto.UserId) is null)
            throw new InvalidOperationException($"User with ID {dto.UserId} doesn't exist");

        var album = dto.Adapt<Album>();

        await _albumRepository.AddAsync(album);
        await _albumRepository.SaveChangesAsync();
        return album.Id;
    }

    public async Task<List<AlbumDto>> GetAllAsync()
    {
        return await _albumRepository.GetAll().ProjectToType<AlbumDto>().ToListAsync();
    }
}
