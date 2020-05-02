using System;
using System.Collections.Generic;

namespace back_end.Entities
{
  public class Playlist
  {
    public Guid GUID {get;set;}
    public Guid OwnerGuiID {get;set;}
    public string PlaylistTitle {get;set;}
    public IEnumerable<Guid> SongIds {get;set;}
  }
}