using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end.Services
{
  public interface IPlaylistService
  {
    Task<PlaylistModel> AddPlaylist(string title, Guid ownerId);
    Task<Playlist> EditPlaylistName(string newTitle, Guid ownerId, Guid playlistId);
    Task<Playlist> GetPlaylist(Guid ownerId, Guid playlistId);
    Task<bool> RemovePlaylist(Guid ownerId, Guid playlistId);
    Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(Guid ownerId);
  }
  public class PlaylistService : IPlaylistService
  {

    private const string collectionName = "playlists";
    private readonly MongoDBCRUD _context;
    private readonly ILogger<PlaylistService> _logger;

    public PlaylistService(IOptions<MongoDBSettings> settings,ILogger<PlaylistService> logger)
    {
      _context = new MongoDBCRUD(settings);
      _logger = logger;
    }

    public async Task<PlaylistModel> AddPlaylist(string title, Guid ownerId)
    {
      try
      {
        PlaylistModel playlist = new PlaylistModel{Id = new Guid(), OwnerId = ownerId, Title = title, SongIds = new List<string>()};
        
        await _context.InsertRecord<PlaylistModel>(collectionName,playlist);

        return playlist;
        
      }
      catch(MongoException e)
      {
        _logger.LogCritical($"Mongo exception {e.Message}");
        return null;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception {e.Message}");
        return null;
      }
    }
    public async Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(Guid ownerId)
    {
      var playlists = await _context.LoadRecords<PlaylistModel>(collectionName);

      var userPlaylists = new List<Playlist>();
      
      foreach( var rec in playlists)
      {
        if(rec.OwnerId.Equals(ownerId))
        {
          userPlaylists.Add(new Playlist{
            Id = rec.Id,
            Title = rec.Title,
            SongIds = rec.SongIds
          });
        }
      }
      return userPlaylists;
    }      
    public async Task<Playlist> EditPlaylistName(string newTitle, Guid ownerId, Guid playlistId)
    {
      try
      {

        var playlist = await _context.LoadRecordById<PlaylistModel>(collectionName, playlistId);
        if(playlist.OwnerId.Equals(ownerId))
        {
          playlist.Title = newTitle;
          var result = await _context.UpsertRecord<PlaylistModel>(collectionName, playlistId,playlist);
          return new Playlist{
            Id = result.Id,
            OwnerId = result.OwnerId,
            Title = result.Title,
            SongIds = result.SongIds
          };  
        }
        else
        {
          return null;
        }      
        
      }
      catch(MongoException e)
      {
        _logger.LogCritical($"Mongo exception {e.Message}");
        return null;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception {e.Message}");
        return null;
      }
    }

    public async Task<Playlist> GetPlaylist(Guid ownerId, Guid playlistId)
    {
      try
      {
        var playlist = await _context.LoadRecordById<PlaylistModel>(collectionName,playlistId);
        if(playlist.OwnerId.Equals(ownerId))
        {
          return new Playlist{
            Id = playlist.Id,
            OwnerId = playlist.OwnerId,
            Title = playlist.Title,
            SongIds = playlist.SongIds
          };
        }
        else
        {
          return null;
        }
      }
      catch(MongoException e)
      {
        _logger.LogCritical($"Mongo exception {e.Message}");
        return null;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General exception {e.Message}");
        return null;
      }
    }

    public async Task<bool> RemovePlaylist(Guid ownerId, Guid playlistId)
    {
      try
      {

        var playlist = await GetPlaylist(ownerId, playlistId);
        if(playlist.OwnerId.Equals(ownerId))
        {
          await _context.DeleteRecord<PlaylistModel>(collectionName, playlistId);
        }

        return true;
      }
      catch(MongoException e)
      {
        _logger.LogCritical($"Mongo Exception {e.Message}");
        return false;
      }
      catch(Exception e)
      {
        _logger.LogCritical($"General Exception {e.Message}");
        return false;
      }
    }
  }
}