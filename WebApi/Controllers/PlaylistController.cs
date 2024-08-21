using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;

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
    public async Task<List<PlaylistDto>> GetPlaylists()
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
    public async Task<int> AddPlaylist(PlaylistDto.Add playlist)
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
    public async Task Update(PlaylistDto playlist)
    {
        await _playlistService.Update(playlist);
    }
}
