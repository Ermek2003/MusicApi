using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet("GetPlaylists")]
    public async Task<List<PlaylistDto>> GetAll()
    {
        return await _playlistService.GetAllAsync();
    }

    [HttpGet("GetById")]
    public async Task<PlaylistDto> GetById(int id)
    {
        return await _playlistService.GetByIdAsync(id);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost("AddPlaylist")]
    public async Task<int> Add(PlaylistAddDto playlist)
    {
        return await _playlistService.AddAsync(playlist);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("DeletePlaylist")]
    public async Task Delete(int id)
    {
        await _playlistService.DeleteAsync(id);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("UpdatePlaylist")]
    public async Task Update(PlaylistEditDto playlist)
    {
        await _playlistService.UpdateAsync(playlist);
    }
}
