using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Contracts.Requests.Playlist;
using back_end.Contracts.Responses.Playlist;
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
    public async Task<ActionResult<IEnumerable<GetPlaylistResponse>>> GetAllPlaylists()
    {
      try{
        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.GetAllPlaylistsOfUser(new Guid(user_guid));
        
        var playlists = new List<GetPlaylistResponse>();

        foreach(var rec in result)
        {
          playlists.Add(
            new GetPlaylistResponse{
              GUID = rec.GUID,
              PlaylistTitle = rec.PlaylistTitle,
              SongIds = rec.SongIds
            }
          );
        }

        return Ok(playlists);

      }catch(Exception e)
      {
        _logger.LogCritical($"Controller exception {e.Message}");
        return StatusCode(500);
      }
    }

    [HttpPut]
    public async Task<ActionResult> EditPlaylist([FromBody] EditPlaylistRequest playlist)
    {
      try
      {

        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.EditPlaylistName(playlist.NewPlaylistTitle,new Guid(user_guid),new Guid(playlist.PlaylistId));

        return Ok(result);

      }
      catch(Exception e)
      {
        _logger.LogCritical($"Controller exception {e.Message}");
        return StatusCode(500);
      }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemovePlaylist(string id)
    {
      try
      {
        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.RemovePlaylist(new Guid(user_guid),new Guid(id));

        return Ok("Removed playlist");
      }catch(Exception e)
      {
        _logger.LogCritical($"Exception {e.Message}");
        return StatusCode(500);
      }
      
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAPlaylist(string id)
    {
      try{
        var user_guid = HttpContext.GetUserUUID();
        var result = await _playlistService.GetPlaylist(new Guid(user_guid),new Guid(id));
    
        return Ok(result);
      }
      catch(Exception e)
      {
        _logger.LogCritical($"Exception {e.Message}");
        return StatusCode(500);
      }
    }

  }
}