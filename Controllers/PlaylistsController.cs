using System;
using System.Threading.Tasks;
using back_end.Contracts.Requests.Playlist;
using back_end.Extensions;
using back_end.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class PlaylistsController :ControllerBase
  {
    private readonly IPlaylistService _playlistService;
    private readonly ILogger<PlaylistsController> _logger;
    
    public PlaylistsController(IPlaylistService service, ILogger<PlaylistsController> logger)
    {
      _playlistService = service;
      _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreatePlaylist([FromBody] AddPlaylistRequest playlist)
    {
      try{

        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.AddPlaylist(playlist.PlaylistName, new Guid(user_guid));
        
        return Ok("Created playlist");
      }catch(Exception e)
      {
        _logger.LogCritical($"Controler Exception ${e.Message}");

        return StatusCode(500);
      }
    }

    [HttpGet]
    public async Task<ActionResult> GetAllPlaylists()
    {
      try{
        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.GetAllPlaylistsOfUser(new Guid(user_guid));

        return Ok(result);

      }catch(Exception e)
      {
        _logger.LogCritical($"Controller exception {e.Message}");
        return StatusCode(500);
      }
    }

    // [HttpGet("{id}")]
    // public Task<ActionResult> GetAPlaylist(string id)
    // {
    //   // returns a playlist and its song ids
    //   return Ok("Playlist");
    // }

    // [HttpPut]
    // public Task<ActionResult> EditPlaylist([FromBody] EditPlaylistRequest playlist)
    // {
    //   return Ok("Edited playlist");
    // }

    // [HttpDelete("{id}")]
    // public Task<ActionResult> RemovePlaylist(string id)
    // {
    //   return Ok();
    // }
    
  }
}