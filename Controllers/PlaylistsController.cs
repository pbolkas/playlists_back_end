using System.Threading.Tasks;
using back_end.Contracts.Requests.Playlist;
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
    private readonly PlaylistService _playlistService;
    private readonly ILogger<PlaylistsController> _logger;
    
    public PlaylistsController(PlaylistService service, ILogger<PlaylistsController> logger)
    {
      _playlistService = service;
      _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreatePlaylist([FromBody] AddPlaylistRequest playlist)
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
    public async Task<ActionResult> EditPlaylist([FromBody] EditPlaylistRequest playlist)
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