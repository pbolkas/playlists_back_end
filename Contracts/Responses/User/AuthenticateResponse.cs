using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Responses.User
{
  public class AuthenticateResponse
  {
    [Required]
    public string Username {get;set;}
    [Required]
    public string Token {get;set;}
  }
}