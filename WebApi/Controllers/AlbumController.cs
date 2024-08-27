using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace WebApi.Controllers;

/// <summary>
/// Controller for work with albums
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet("GetAll")]
    public async Task<List<AlbumDto>> GetAll()
    {
        return await _albumService.GetAllAsync();
    }

    [HttpPost("AddAlbum")]
    public async Task<int> Add(AlbumAddDto dto)
    {
        return await _albumService.AddAsync(dto);
    }

    [HttpGet("GetById")]
    public async Task<AlbumDto> GetById(int id)
    {
        return await _albumService.GetByIdAsync(id);
    }

    [HttpPut("Update")]
    public async Task Update(AlbumEditDto dto)
    {
        await _albumService.UpdateAsync(dto);
    }

    [HttpDelete("Delete")]
    public async Task Delete(int id)
    {
        await _albumService.DeleteAsync(id);
    }
}
