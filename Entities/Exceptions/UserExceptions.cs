namespace back_end.Entities.Exceptions
{
  [System.Serializable]
  public class UserNotFoundException : System.Exception
  {
      public UserNotFoundException() { }
      public UserNotFoundException(string message) : base(message) { }
      public UserNotFoundException(string message, System.Exception inner) : base(message, inner) { }
      protected UserNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }


  [System.Serializable]
  public class UserInvalidCredentialsException : System.Exception
  {
      public UserInvalidCredentialsException() { }
      public UserInvalidCredentialsException(string message) : base(message) { }
      public UserInvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
      protected UserInvalidCredentialsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
    
}