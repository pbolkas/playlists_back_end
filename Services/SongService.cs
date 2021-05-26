using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace back_end.Services
{
  public interface ISongService
  {
    Task<Song> AddSongAsync(Song s,string playlistId);
    Task<bool> EditSongTitleAsync(Song s);
    Task<Song> GetSongAsync(string id);
    Task<SongInfo> GetSongInfoAsync(string id);
    Task<bool> RemoveSongAsync(string songId, string playlistId);
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

    public async Task<Song> AddSongAsync(Song s,string playlistId)
    {
      try
      {
        var songId = await _context.StoreFileAsync(s.SongTitle, s.SongBytes,null);
        s.Id= songId.ToString();

        var result = await AddSongToPlaylistAsync(s.Id,playlistId);

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

    public async Task<SongInfo> GetSongInfoAsync(string id)
    {
      try
      {
        var songInfo = await _context.FindFileInfoAsync(id);
        
        return new SongInfo{
          Id = songInfo.Id.ToString(),
          SongTitle = songInfo.Filename
        };
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song info read {e.Message}");
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


    public async Task<bool> RemoveSongAsync(string songId,string playlistId)
    {
      try
      {
        var result = await _context.RemoveFileAsync(songId);
        if(result)
        {
          await RemoveSongFromPlaylistAsync(songId,playlistId);
        }
        return result;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song edit title {e.Message}");
        return false;
      }
    }

    private async Task<bool> SongExistsAsync(string songId,string playlistId)
    {
      try
      {
        var playlist = await _context.LoadRecordById<PlaylistModel>("playlists", new Guid(playlistId));
        if(playlist == null)
        {
          return false;
        }
        // TODO: find song inside playlist songIds
        return true;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"Exception on song find {e.Message}");
        return false;
      }
    }

    private async Task<bool> RemoveSongFromPlaylistAsync(string songId,string playlistId)
    {
      try
      {
        var playlist = await _context.LoadRecordById<PlaylistModel>("playlists", new Guid(playlistId));
        if(playlist == null)
        {
          return false;
        }
        var removed = playlist.SongIds.Remove(songId);

        if(removed)
        {
          var result = await _context.UpsertRecord<PlaylistModel>("playlists",new Guid(playlistId), playlist);
          return true;
        }

        return false;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on song removal from playlis {e.Message}");
        return false;
      }
    }

    private async Task<bool> AddSongToPlaylistAsync(string songId,string playlistId)
    {
      try
      {
        var playlist = await _context.LoadRecordById<PlaylistModel>("playlists", new Guid(playlistId));
        
        List<string> ids = playlist.SongIds;
        if(ids ==null)
        {
          ids = new List<string>();
        }
        ids.Add(songId);
        playlist.SongIds = ids;
        
        var result = await _context.UpsertRecord<PlaylistModel>("playlists",new Guid(playlistId), playlist);
        return true;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception on add new songh to playlist {e.Message}");
        return false;
      }
    }
  }
}