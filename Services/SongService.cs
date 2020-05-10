using System;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Helpers;
using back_end.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace back_end.Services
{
  public interface ISongService
  {
    Task<Song> AddSong(Song s);
    Task EditSong(Song s);
    Task<Song> GetSongAsync(string id);
    Task RemoveSong();
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

    public async Task<Song> AddSong(Song s)
    {
      try
      {
        await _context.storeFileAsync(s.SongTitle, s.SongBytes);
        return s;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song add ${e.Message}");
        return null;
      }
    }
    public async Task<Song> GetSongAsync(string id)
    {
      try
      {
        var song =  await _context.retrieveFileAsync(new ObjectId("5eb824294e592e629f53c839"));
        return new Song{
          SongBytes = song
        };
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song read {e.Message}");
        return null;
      }
    }

    public Task EditSong(Song s)
    {
      throw new System.NotImplementedException();
    }


    public Task RemoveSong()
    {
      throw new System.NotImplementedException();
    }
  }
}