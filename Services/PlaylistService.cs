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
    Task<PlaylistModel> AddPlaylist(string title, Guid ownerID);
    Task<Playlist> EditPlaylistName(string newTitle, Guid ownerId, Guid playlistId);
    Task<Playlist> GetPlaylist(Guid ownerId, Guid playlistId);
    Task<bool> RemovePlaylist(Guid ownerId, Guid playlistID);
    Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(Guid ownerID);
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

    public async Task<PlaylistModel> AddPlaylist(string title, Guid ownerID)
    {
      try{
        PlaylistModel playlist = new PlaylistModel{PlaylistId = new Guid(), OwnerId = ownerID, Title = title};
        
        await _context.InsertRecord<PlaylistModel>(collectionName,playlist);

        return playlist;
        
      }catch(MongoException e)
      {
        _logger.LogCritical($"Mongo exception {e.Message}");
        return null;
      }catch(Exception e)
      {
        _logger.LogCritical($"General exception {e.Message}");
        return null;
      }
    }
    public async Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(Guid ownerID)
    {
      var playlists = await _context.LoadRecords<PlaylistModel>(collectionName);

      var userPlaylists = new List<Playlist>();
      
      foreach( var rec in playlists)
      {
        if(rec.OwnerId.Equals(ownerID))
        {
          userPlaylists.Add(new Playlist{
            GUID = rec.PlaylistId,
            PlaylistTitle = rec.Title,
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

        var playlist = await _context.LoadRecordById<Playlist>(collectionName, playlistId);
        if(playlist.OwnerGuiID.Equals(ownerId))
        {
          playlist.PlaylistTitle = newTitle;
          await _context.UpsertRecord<Playlist>(collectionName,playlistId,playlist);
          
        }
        else
        {
          return null;
        }
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

    public async Task<Playlist> GetPlaylist(Guid ownerID, Guid playlistId)
    {
      try{
        var playlist = await _context.LoadRecordById<Playlist>(collectionName,playlistId);
        if(playlist.OwnerGuiID.Equals(ownerID))
        {
          return playlist;
        }else{
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


    public async Task<bool> RemovePlaylist(Guid ownerId, Guid playlistID)
    {
      try
      {
        var playlist = await GetPlaylist(ownerId,playlistID);
        if(playlist.OwnerGuiID.Equals(ownerId))
        {
          await _context.DeleteRecord<Playlist>(collectionName,playlistID);
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