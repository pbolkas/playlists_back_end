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
  }
}