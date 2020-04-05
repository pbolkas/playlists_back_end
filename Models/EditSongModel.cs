using System.ComponentModel.DataAnnotations;

namespace back_end.Models
{
  public class EditSongModel
  {
    [Required]
    public string SongId{get;set;}
    [Required]
    public string NewTitle{get;set;}

  }
}