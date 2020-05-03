using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Playlist
{
  public class EditPlaylistRequest
  {
    [Required]
    public string Id {get;set;}
    [Required]
    public string NewTitle{get;set;}
  }
}