using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Contracts.Requests.Playlist;
using back_end.Contracts.Responses.Errors;
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
  public class PlaylistsController : ControllerBase
  {
    private readonly IPlaylistService _playlistService;
    private readonly ISongService _songService;
    private readonly ILogger<PlaylistsController> _logger;

    public PlaylistsController(IPlaylistService service, ISongService songService, ILogger<PlaylistsController> logger)
    {
      _playlistService = service;
      _songService = songService;
      _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<AddPlaylistResponse>> CreatePlaylist([FromBody] AddPlaylistRequest playlist)
    {
      try
      {

        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.AddPlaylist(playlist.Title, new Guid(user_guid));

        if (result == null)
        {
          return BadRequest(
            new Error
            {
              ErrorDescription = "There has been an error while creating a new playlist"
            }
          );
        }

        return Ok(new AddPlaylistResponse
        {
          Id = result.Id,
          Title = result.Title,
          SongIds = new List<string>()
        });

      }
      catch (Exception e)
      {
        _logger.LogCritical($"Controler Exception ${e.Message}");

        return StatusCode(500);
      }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPlaylistResponse>>> GetAllPlaylists()
    {
      try
      {
        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.GetAllPlaylistsOfUser(new Guid(user_guid));

        var playlists = new List<GetPlaylistResponse>();

        foreach (var rec in result)
        {
          playlists.Add(
            new GetPlaylistResponse
            {
              Id = rec.Id,
              Title = rec.Title,
              Songs = await getSongsOfPlaylist(rec.SongIds),
            }
          );
        }

        return Ok(playlists);

      }
      catch (Exception e)
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

        var result = await _playlistService.EditPlaylistName(playlist.NewTitle, new Guid(user_guid), new Guid(playlist.Id));

        return Ok(
          new EditPlaylistResponse
          {
            SongId = result.Id.ToString(),
            NewTitle = result.Title
          }
        );

      }
      catch (Exception e)
      {
        _logger.LogCritical($"Controller exception {e.Message}");
        return StatusCode(500);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<RemovePlaylistResponse>> RemovePlaylist(string id)
    {
      try
      {
        var user_guid = HttpContext.GetUserUUID();

        var result = await _playlistService.RemovePlaylist(new Guid(user_guid), new Guid(id));

        return Ok(
        new RemovePlaylistResponse
        {
          PlaylistId = id
        });
      }
      catch (Exception e)
      {
        _logger.LogCritical($"Exception {e.Message}");
        return StatusCode(500);
      }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAPlaylist(string id)
    {
      try
      {
        var user_guid = HttpContext.GetUserUUID();
        var result = await _playlistService.GetPlaylist(new Guid(user_guid), new Guid(id));

        return Ok(
          new GetPlaylistResponse
          {
            Id = result.Id,
            Songs = await getSongsOfPlaylist(result.SongIds),
            Title = result.Title
          }
        );
      }
      catch (Exception e)
      {
        _logger.LogCritical($"Exception {e.Message}");
        return StatusCode(500);
      }
    }
    private async Task<IEnumerable<Song>> getSongsOfPlaylist(IEnumerable<string> ids)
    {
      var songs = new List<Song>();

      foreach (var song in ids)
      {
        var songInfo = await _songService.GetSongInfoAsync(song);
        songs.Add(
          new Song
          {
            SongId = songInfo.Id,
            SongTitle = songInfo.SongTitle
          });
      }

      return songs;
    }

  }
}