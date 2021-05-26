using System;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace back_end.Entities
{
  public class Song
  {
    public string Id{get;set;}
    public string SongTitle{get;set;}
    public byte [] SongBytes{get;set;}
  }
}