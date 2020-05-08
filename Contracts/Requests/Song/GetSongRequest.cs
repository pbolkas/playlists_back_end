using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Song
{
  public class GetSongRequest
  {
    [Required]
    public string SongId {get;set;}
  }
}