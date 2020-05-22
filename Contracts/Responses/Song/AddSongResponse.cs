using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Responses.Song
{
  public class AddSongResponse
  {
    [Required]
    public string Result {get;set;}
  }
}