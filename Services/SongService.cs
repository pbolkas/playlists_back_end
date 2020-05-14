using System;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace back_end.Services
{
  public interface ISongService
  {
    Task<Song> AddSongAsync(Song s);
    Task<bool> EditSongTitleAsync(Song s);
    Task<Song> GetSongAsync(string id);
    Task<bool> RemoveSongAsync(string id);
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

    public async Task<Song> AddSongAsync(Song s)
    {
      try
      {
        var songId = await _context.StoreFileAsync(s.SongTitle, s.SongBytes,null);
        s.Id= songId.ToString();

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
        var songInfo = await _context.FindFileInfoAsync(id);
        
        var song =  await _context.RetrieveFileAsync(id);

        return new Song{
          SongBytes = song,
          SongTitle = songInfo.Filename
        };
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song read {e.Message}");
        return null;
      }
    }

    public async Task<bool> EditSongTitleAsync(Song s)
    {
      try
      {
        var result = await _context.EditFileNameAsync(s.Id,s.SongTitle);

        return result;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song edit title {e.Message}");
        return false;
      }
    }


    public async Task<bool> RemoveSongAsync(string id)
    {
      try
      {
        var result = await _context.RemoveFileAsync(id);
        
        return result;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song edit title {e.Message}");
        return false;
      }
    }
  }
}