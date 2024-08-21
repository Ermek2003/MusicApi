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
    public async Task<List<AlbumDto>> GetAllAsync()
    {
        return await _albumService.GetAllAsync();
    }

    [HttpGet("GetById")]
    public async Task GetById(int dto)
    {

    }

    ////[HttpPost("AddAlbum")]
    ////public async Task<int> AddAlbum([FromBody] AlbumDto.Add dto)
    ////{
    ////    return await _albumService.AddAsync(dto);
    ////}
}
