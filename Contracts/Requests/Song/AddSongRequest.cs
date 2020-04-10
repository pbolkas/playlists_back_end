using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace back_end.Contracts.Requests.Song
{
  public class AddSongRequest
  {
    [Required]
    public string PlaylistId {get;set;}
    [Required]
    public string SongTitle{get;set;}
    [Required]
    public IFormFile SongBytes {get;set;} 
  }
}