using System;
using System.IO;
using System.Threading.Tasks;
using back_end.Contracts.Requests.Song;
using back_end.Contracts.Responses.Song;
using back_end.Entities;
using back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
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
        // TODO: remove this
        songId = "5eb824294e592e629f53c839";
        
        var song = await _songService.GetSongAsync(songId);

        return File(song.SongBytes, "application/octet-stream","a.mp3");
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
      try{
        var ms = new MemoryStream();
        request.SongBytes.CopyTo(ms);
        var bytes = ms.ToArray();

        // first add song to gridfs
        await _songService.AddSong(new Song{
          SongBytes= bytes,
          SongTitle = request.SongTitle,
        });

        // then write guid of song to the equivalent playlist

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
    public async Task<ActionResult> EditSongTitle([FromBody]EditSongRequest request)
    {
      try
      {
        return Ok("Edited a song");
      }
      catch(Exception e)
      {
        return StatusCode(500);
      }
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveSong(RemoveSongRequest request)
    {
      try
      {
        return Ok("Removed a song");
      }
      catch(Exception e)
      {
        return StatusCode(500);
      }
    }

  }
}