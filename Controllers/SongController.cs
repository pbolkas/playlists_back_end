using System.Threading.Tasks;
using back_end.Contracts.Requests.Song;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SongController:ControllerBase
  {
    private readonly SongService _songService;
    private readonly ILogger<SongController> _logger;
    public SongController(SongService service, ILogger<SongController> logger)
    {
      _songService = service;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetSong(GetSongRequest request){
      return Ok("Get a song");
    }

    [HttpPost ]
    public async Task<ActionResult> AddSong([FromBody]AddSongRequest request)
    {
      return Ok("added song");
    }

    [HttpPut]
    public async Task<ActionResult> EditSongTitle([FromBody]EditSongRequest request)
    {
      return Ok("Edited song title");
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveSong(RemoveSongRequest request)
    {
      return Ok("removed the song");
    }

  }
}