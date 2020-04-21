using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Helpers;
using Microsoft.Extensions.Logging;

namespace back_end.Services
{
  public interface IPlaylistService
  {
    Task<Playlist> AddPlaylist(string title);
    Task<Playlist> EditPlaylistName(string newTitle, Guid playlistId);
    Task<Playlist> GetPlaylist(string title, Guid playlistID);
    Task<IEnumerable<Playlist>> GetAllPlaylists();
    Task<bool> RemovePlaylist(Guid playlistID);
  }
  public class PlaylistService : IPlaylistService
  {
    // private readonly MongoContext _context;
    private readonly AppSettings _appSettings;
    private readonly ILogger<PlaylistService> _logger;

    public PlaylistService(AppSettings appSettings, ILogger<PlaylistService> logger)
    {
      _logger = logger;
      _appSettings = appSettings;
    }

    public Task<Playlist> AddPlaylist(string title)
    {
      throw new NotImplementedException();
    }

    public Task<Playlist> EditPlaylistName(string newTitle, Guid playlistId)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Playlist>> GetAllPlaylists()
    {
      throw new NotImplementedException();
    }

    public Task<Playlist> GetPlaylist(string title, Guid playlistID)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RemovePlaylist(Guid playlistID)
    {
      throw new NotImplementedException();
    }
  }
}