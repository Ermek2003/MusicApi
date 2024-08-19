using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<List<Playlist>> GetPlaylists()
    {
        return await _playlistService.GetAllAsync();
    }

    [HttpGet("GetById")]
    public async Task<Playlist> GetById(int id)
    {
        return await _playlistService.GetByIdAsync(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AddPlaylist")]
    public async Task<int> AddPlaylist(Playlist playlist)
    {
        return await _playlistService.AddAsync(playlist);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeletePlaylist")]
    public async Task<int> Delete(int id)
    {
        return await _playlistService.DeleteAsync(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("UpdatePlaylist")]
    public async Task<int> Update(Playlist playlist)
    {
        return await _playlistService.Update(playlist);
    }
}
