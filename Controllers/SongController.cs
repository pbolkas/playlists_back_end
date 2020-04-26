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

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSong(string id){
      return Ok("Get a song");
    }

    [HttpPost ]
    public async Task<ActionResult> AddSong([FromBody]AddSongRequest song)
    {
      return Ok("added song");
    }

    [HttpPut]
    public async Task<ActionResult> EditSongTitle([FromBody]EditSongRequest song)
    {
      return Ok("Edited song title");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveSong(string id)
    {
      return Ok("removed the song");
    }

  }
}