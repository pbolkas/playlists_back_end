using System.Linq;
using Microsoft.AspNetCore.Http;

namespace back_end.Extensions
{
  public static class GeneralExtensions
  {
    public static string GetUserUUID(this HttpContext httpContext)
    {
      if(httpContext.User == null)
      {
        return string.Empty;
      }

      return httpContext.User.Claims.Single(x=> x.Type =="uuid").Value;
    }

    public static string GetUserRole(this HttpContext httpContext)
    {
      if (httpContext.User == null)
      {
        return string.Empty;
      }

      return httpContext.User.Claims.Single(x => x.Type == "role").Value;
    }
  }
}