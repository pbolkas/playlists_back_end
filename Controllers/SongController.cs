using System;
using System.IO;
using System.Threading.Tasks;
using back_end.Contracts.Requests.Song;
using back_end.Contracts.Responses.Errors;
using back_end.Contracts.Responses.Song;
using back_end.Entities;
using back_end.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class SongController:ControllerBase
  {
    private readonly ISongService _songService;
    private readonly ILogger<SongController> _logger;
    public SongController(ISongService service, ILogger<SongController> logger)
    {
      _songService = service;
      _logger = logger;
    }

    [HttpGet("{songId}")]
    public async Task<ActionResult> GetSong(string songId){
      try
      {        
        var song = await _songService.GetSongAsync(songId);

        return Ok(File(song.SongBytes, "application/octet-stream",$"{song.SongTitle}.mp3"));
      }
      catch(Exception e)
      {
        _logger.LogCritical($"Controller exception on get song {e.Message}");
        return StatusCode(500);
      }
    }

    [HttpPost]
    public async Task<ActionResult> AddSong([FromForm]AddSongRequest request)
    {
      try
      {
        var ms = new MemoryStream();
        request.SongBytes.CopyTo(ms);
        var bytes = ms.ToArray();

        // first add song to gridfs
        await _songService.AddSongAsync(new Song{
          SongBytes= bytes,
          SongTitle = request.SongTitle,
        });

        // TODO: then write object id of song to the equivalent playlist
        
        return Ok(new AddSongResponse{
          result = $"Added Song {request.SongTitle} Successfully"
        });
      }
      catch(Exception e)
      {
        _logger.LogCritical($"Exception on song add controller {e.Message}");
        return StatusCode(500);
      }
    }

    [HttpPut]
    public async Task<ActionResult> EditSongTitle([FromBody]EditSongTitleRequest request)
    {
      try
      {
        var result = await _songService.EditSongTitleAsync(
          new Song{
            Id = request.SongId,
            SongTitle = request.NewTitle
          });

        if(!result)
        {
          return BadRequest( new Error{
            ErrorDescription = "Could not edit song title"
          });
        }

        return Ok(
          new EditSongTitleResponse{
          NewTitle = request.NewTitle,
          SongId = request.SongId
        });
      }
      catch(Exception e)
      {
        _logger.LogCritical($"Exception on song edit title controller {e.Message}");
        return StatusCode(500);
      }
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveSong(RemoveSongRequest request)
    {
      try
      {
        var result = await _songService.RemoveSongAsync(request.SongId);

        if(!result)
        {
          return BadRequest( new Error{
            ErrorDescription = "Could not remove song"
          });
        }

        return  Ok(new RemoveSongResponse{
          Details = "Successfully removed song"
        });
      }
      catch(Exception e)
      {
        _logger.LogCritical($"Exception on song remove controller {e.Message}");
        return StatusCode(500);
      }
    }

  }
}