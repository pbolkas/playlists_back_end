using System.ComponentModel.DataAnnotations;

namespace back_end.Contracts.Requests.User
{
  public class UserSubscribeRequest
  {
    [Required]
    public string Username {get;set;}
    [Required]
    public string Email {get;set;}
    [Required]
    public string Password {get;set;}
  }
}