using System;

namespace back_end.Entities
{
  public class User
  {
    public Guid GUID {get;set;}
    public string Username {get;set;}
    public string Email {get;set;}
    public string Password{get;set;}
    public string Token {get;set;}
    
  }
}