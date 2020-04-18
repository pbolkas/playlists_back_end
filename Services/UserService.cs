using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using back_end.Entities;
using back_end.Helpers;
using back_end.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace back_end.Services
{
  public interface IUserService
  {
    Task<User> Authenticate(string username,string password);
    Task<User> Register(User u);
    
  }

  public class UserService : IUserService
  {
    private readonly MySQLContext _context;
    private readonly AppSettings _appSettings;
    private readonly ILogger<UserService> _logger;
    public UserService(MySQLContext context, IOptions<AppSettings> appSettings,ILogger<UserService> logger)
    {
      _context = context;
      _appSettings = appSettings.Value;
      _logger = logger;
      
      context.Database.EnsureCreated();
    }
    public async Task<User> Authenticate(string username, string password)
    {
      try{
        var user = await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        if(user == null) return null;

        var passwordIsCorrect = bcrypt.Verify(password, user.Hash);

        if(!passwordIsCorrect) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor{
          Subject =new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("username",user.Username)
          }),
          Expires = DateTime.UtcNow.AddHours(9),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new User{Username = username, Email = user.Email, Token = tokenHandler.WriteToken(token)};

      }catch(Exception e){
        _logger.LogCritical($"Exception during authentication {e.Message}");
        return null;
      }
    }

    public async Task<User> Register(User u)
    {
      try{
        _context.Users.Add(new UserModel{
          Email = u.Email,
          Username = u.Username,
          Hash = bcrypt.HashPassword(u.Password, workFactor:12),
          Role = Roles.User,
          UserId = Guid.NewGuid()
        });
        await _context.SaveChangesAsync();
        u.Password = "";
        return u;
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