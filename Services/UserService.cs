using
using System;
using System.Threading.Tasks;
using back_end.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace back_end.Services
{
  public interface IUserService
  {
    Task<User> Authenticate(string email,string password);

    Task<User> Register(User u);
    
  }

  public class UserService : IUserService
  {
    private readonly ILogger<UserService> _logger;
    public UserService(ILogger<UserService> logger)
    {
      _logger = logger;
    }
    public async Task<User> Authenticate(string email, string password)
    {
      try{

      }catch(Exception e){
        _logger.LogCritical($"Exception during authentication {e.Message}");
        return null;
      }
    }

    public async Task<User> Register(User u)
    {
      try{

      }catch(DbUpdateException e)
      {
        _logger.LogCritical($"DB Exception on user register -> {e.Message}");
        return null;
      }catch(Exception e)
      {
        _logger.LogCritical($"Exception on user register -> {e.Message}");
        return null;
      }
    }
  }
}