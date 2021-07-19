using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
  // The Model Which will be used for the Database
  [Table("users")]
  public class UserModel
  {
    [Required]
    [Key]
    [Column("id")]
    public int Id{get;set;}
    
    // This is the id which maps to the user's playlist
    [Required]
    [Column("user_id",TypeName = "varchar(255)")]
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

#nullable enable
    [Column("verification_token", TypeName = "varchar(90)")]
    public string? Token {get;set;}

    [Column("email_verified")]
    public bool? EmailConfirmed {get;set;}
#nullable disable

    [Required]
    [Column("role",TypeName = "varchar(25)")]
    public string Role{get;set;}
  }
}