using System;
using System.Collections.Generic;

namespace back_end.Contracts.Responses.Playlist
{
  public class GetPlaylistResponse
  {

    public Guid Id {get;set;}
    public string Title {get;set;}
    public IEnumerable<Song> Songs {get;set;}

  }

  public class Song 
  {
    public string SongId{get;set;}
    public string SongTitle{get;set;}
  }
}