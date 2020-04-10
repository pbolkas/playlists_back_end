namespace back_end.Contracts.Requests.User
{
  public class AuthenticateRequest
  {
    public string Username {get;set;}
    public string Password {get;set;}
  }
}