using System.ComponentModel.DataAnnotations;

namespace back_end.Models
{
  public class SubscriptionModel
  {
    [Required]
    public string Username {get;set;}
    [Required]
    public string Email {get;set;}
    [Required]
    public string Password {get;set;}
  }
}