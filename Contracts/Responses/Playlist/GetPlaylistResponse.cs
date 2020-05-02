using System;
using System.Collections.Generic;

namespace back_end.Contracts.Responses.Playlist
{
  public class GetPlaylistResponse
  {

    public Guid GUID {get;set;}
    public string PlaylistTitle {get;set;}
    public IEnumerable<Guid> SongIds {get;set;} 

  }
}