
using System.ComponentModel.DataAnnotations;
namespace back_end.Models
{
  public class AuthenticateModel
  {
    [Required]
    public string Email {get;set;}
    [Required]
    public string Password{get;set;}
  }
}