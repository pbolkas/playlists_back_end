using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Song
{
  public class RemoveSongRequest
  {
    [Required]
    public string PlaylistId {get;set;}
  }
}