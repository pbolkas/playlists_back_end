using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace back_end.Models
{
  public class PlaylistModel
  {
    [BsonId]
    public Guid PlaylistId {get;set;}
    public Guid OwnerId {get;set;}
    public string Title {get;set;}
    public IEnumerable<Guid> SongIds{get;set;}
  }
}