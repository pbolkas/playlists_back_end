using back_end.Helpers;
using Microsoft.Extensions.Logging;

namespace back_end.Services
{
  public interface ISongService
  {

  }

  public class SongService : ISongService
  {
    private readonly ILogger<SongService> _logger;

    public SongService(ILogger<SongService> logger)
    {
      _logger = logger;
    }
    
  }
}