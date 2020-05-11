using System;
using System.Collections.Generic;

namespace back_end.Entities
{
  public class Playlist
  {
    public Guid Id {get;set;}
    public Guid OwnerId {get;set;}
    public string Title {get;set;}
    public IEnumerable<string> SongIds {get;set;}
  }
}