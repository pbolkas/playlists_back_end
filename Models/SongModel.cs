using System;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;

namespace back_end.Models
{
  public class SongModel
  {
    [BsonId]
    public Guid SongId{get;set;}
    public string Title {get;set;}
    public byte[] SongBytes {get;set;}
  }
}