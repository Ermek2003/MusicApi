using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrackController : ControllerBase
{
    private readonly ITrackService _trackService;

    public TrackController(ITrackService trackService)
    {
        _trackService = trackService;
    }

    [HttpGet("GetAll")]
    public async Task<List<TrackDto>> GetAll()
    {
        return await _trackService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<TrackDto> GetById(int id)
    {
        return await _trackService.GetByIdAsync(id);
    }

    [HttpPost("Add")]
    public async Task<int> Add([FromForm] TrackAddDto dto)
    {
        return await _trackService.AddAsync(dto);
    }

    [HttpPut("Edit")]
    public async Task Edit([FromForm] TrackEditDto dto)
    {
        await _trackService.UpdateAsync(dto);
    }

    [HttpDelete("Delete")]
    public async Task Delete(int id)
    {
        await _trackService.DeleteAsync(id);
    }
}
