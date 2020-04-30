using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Helpers;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace back_end.Services
{
  public interface IPlaylistService
  {
    Task<Playlist> AddPlaylist(string title, Guid ownerID);
    Task<Playlist> EditPlaylistName(string newTitle, Guid playlistId);
    Task<Playlist> GetPlaylist(string title, Guid playlistID);
    Task<IEnumerable<Playlist>> GetAllPlaylists(Guid ownerID);
    Task<bool> RemovePlaylist(Guid playlistID);
  }
  public class PlaylistService : IPlaylistService
  {
    private readonly IMongoCollection<Playlist> _playlists;
    private readonly AppSettings _appSettings;
    private readonly ILogger<PlaylistService> _logger;

    public PlaylistService(ILogger<PlaylistService> logger)
    {
      
      _logger = logger;
    }

    public async Task<Playlist> AddPlaylist(string title, Guid ownerID)
    {
      Playlist playlist = new Playlist{GUID = new Guid(), OwnerGuiID = ownerID, PlaylistTitle = title};
      await _playlists.InsertOneAsync(playlist);

      return playlist;
    }

    public Task<Playlist> EditPlaylistName(string newTitle, Guid playlistId)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Playlist>> GetAllPlaylists(Guid ownerID)
    {
      return await _playlists.Find(playlist => playlist.OwnerGuiID.Equals(ownerID)).ToListAsync();
    }

    public async Task<Playlist> GetPlaylist(string title, Guid ownerID)
    {
      return await _playlists.Find<Playlist>(playlist =>
       playlist.PlaylistTitle == title 
       && ownerID.Equals(playlist.OwnerGuiID))
       .FirstOrDefaultAsync();
    }

    public Task<bool> RemovePlaylist(Guid playlistID)
    {
      throw new NotImplementedException();
    }
  }
}