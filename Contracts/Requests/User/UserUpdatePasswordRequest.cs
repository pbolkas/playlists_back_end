namespace back_end.Contracts.Requests.User
{
  public class UserUpdatePasswordRequest
  {
    public string NewPassword {get;set;}
    public string NewPasswordValidation{get;set;}

  }
}