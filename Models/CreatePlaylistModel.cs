using System.ComponentModel.DataAnnotations;

namespace back_end.Models
{
  public class CreatePlaylistModel
  {
    [Required]
    public string PlaylistName {get;set;}
  }
}