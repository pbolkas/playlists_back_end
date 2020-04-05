using System.ComponentModel.DataAnnotations;

namespace back_end.Models
{
  public class EditPlaylistModel
  {
    [Required]
    public string PlaylistId {get;set;}
    [Required]
    public string NewPlaylistTitle{get;set;}
    
  }
}