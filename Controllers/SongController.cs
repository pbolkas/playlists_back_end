using System.Threading.Tasks;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SongController:ControllerBase
  {
    private readonly ILogger<SongController> _logger;
    public SongController(ILogger<SongController> logger)
    {
      _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSong(string id){
      return Ok("Get a song");
    }

    [HttpPost ]
    public async Task<ActionResult> AddSong([FromBody]AddSongModel song)
    {
      return Ok("added song");
    }

    [HttpPut]
    public async Task<ActionResult> EditSongTitle([FromBody]EditSongModel song)
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