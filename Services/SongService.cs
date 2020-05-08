using back_end.Helpers;
using back_end.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace back_end.Services
{
  public interface ISongService
  {

  }

  public class SongService : ISongService
  {
    private readonly ILogger<SongService> _logger;
    private readonly MongoDBCRUD _context;

    public SongService(IOptions<MongoDBSettings> settings,ILogger<SongService> logger)
    {
      _context = new MongoDBCRUD(settings);
      _logger = logger;
    }
    
  }
}