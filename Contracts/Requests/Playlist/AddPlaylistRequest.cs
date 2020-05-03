using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Playlist
{
  public class AddPlaylistRequest
  {
    [Required]
    public string Title {get;set;}
  }
}