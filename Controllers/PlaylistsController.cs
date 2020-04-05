using System.Threading.Tasks;
using back_end.Models;
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
    private readonly ILogger<PlaylistsController> _logger;
    
    public PlaylistsController(ILogger<PlaylistsController> logger)
    {
      _logger = logger;      
    }

    [HttpPost]
    public async Task<ActionResult> CreatePlaylist([FromBody] CreatePlaylistModel playlist)
    {
      return Ok("Created playlist");
    }

    [HttpGet]
    public async Task<ActionResult> GetAllPlaylists()
    {
      return Ok("all playlists");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAPlaylist(string id)
    {
      // returns a playlist and its song ids
      return Ok("Playlist");
    }

    [HttpPut]
    public async Task<ActionResult> EditPlaylist([FromBody] EditPlaylistModel playlist)
    {
      return Ok("Edited playlist");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemovePlaylist(string id)
    {
      return Ok();
    }
    
  }
}