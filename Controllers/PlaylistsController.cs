using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlaylistsController :ControllerBase
  {
    private readonly ILogger<PlaylistsController> _logger;
    
    public PlaylistsController(ILogger<PlaylistsController> logger)
    {
      _logger = logger;      
    }

    
  }
}