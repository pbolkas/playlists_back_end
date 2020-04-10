using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Playlist
{
  public class EditPlaylistRequest
  {
    [Required]
    public string PlaylistId {get;set;}
    [Required]
    public string NewPlaylistTitle{get;set;}
  }
}