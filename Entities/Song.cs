using System;
using Microsoft.AspNetCore.Http;

namespace back_end.Entities
{
  public class Song
  {
    public Guid GUID{get;set;}
    public string SongTitle{get;set;}
    public IFormFile SongBytes{get;set;}
  }
}