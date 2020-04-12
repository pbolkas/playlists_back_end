using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
  // The Model Which will be used for the Database
  public class User
  {
    [Required]
    [Key]
    [Column("id")]
    public int Id{get;set;}
    
    // This is the id which maps to the user's playlist
    [Required]
    [Column("user_id",TypeName = "varchar(30)")]
    public Guid UserId {get;set;}
    
    [Required]
    [Column("username",TypeName = "varchar(30)")]
    public string Username {get;set;}
    
    [Required]
    [EmailAddress]
    [Column("email",TypeName = "varchar(40)")]
    public string Email{get;set;}
    
    [Required]
    [Column("hash",TypeName = "varchar(255)")]
    public string Hash {get;set;}
  }
}