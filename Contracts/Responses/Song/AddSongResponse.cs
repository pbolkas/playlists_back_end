using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Responses.Song
{
  public class AddSongResponse
  {
    [Required]
    public string result {get;set;}
    public string errorDetails {get;set;}
  }
}