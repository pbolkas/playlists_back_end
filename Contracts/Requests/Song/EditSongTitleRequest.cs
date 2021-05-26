using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.Song
{
  public class EditSongTitleRequest
  {
    [Required]
    public string SongId{get;set;}
    [Required]
    public string NewTitle{get;set;}
  }
}