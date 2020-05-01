using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end.Services
{
  public interface IPlaylistService
  {
    Task<PlaylistModel> AddPlaylist(string title, Guid ownerID);
    Task<Playlist> EditPlaylistName(string newTitle, Guid playlistId);
    Task<Playlist> GetPlaylist(string title, Guid playlistID);
    Task<bool> RemovePlaylist(Guid playlistID);
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
        
      }catch(MongoClientException e)
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
            PlaylistTitle = rec.Title
          });
        }
      }
      
      return userPlaylists;
    }
    public async Task<Playlist> GetPlaylist(string title, Guid ownerID)
    {
      return await _context.LoadRecordById<Playlist>(collectionName,ownerID);
    }

    public Task<Playlist> EditPlaylistName(string newTitle, Guid playlistId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RemovePlaylist(Guid playlistID)
    {
      throw new NotImplementedException();
    }
  }
}