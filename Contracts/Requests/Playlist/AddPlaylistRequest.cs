using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Playlist
{
  public class AddPlaylistRequest
  {
    [Required]
    public string PlaylistName {get;set;}
  }
}