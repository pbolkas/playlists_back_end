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
using System.Security.Cryptography;
using bcrypt = BCrypt.Net.BCrypt;
using back_end.Entities.Exceptions;

namespace back_end.Services
{
  public interface IUserService
  {
    Task<User> Authenticate(string username, string password);
    Task<User> Register(User u);
    Task<User> UpdatePassword(string newPassword1, string newPassword2);
    Task<User> UpdateUsername(string newUsername);
    Task<User> RemoveUser(string username);
    Task<User> EmailVerifyAsync(string token);
  }

  public class UserService : IUserService
  {
    private readonly MySQLContext _context;
    private readonly AuthenticationAppSettings _appSettings;
    private readonly ILogger<UserService> _logger;
    public UserService(MySQLContext context, IOptions<AuthenticationAppSettings> appSettings, ILogger<UserService> logger)
    {
      _context = context;
      _appSettings = appSettings.Value;
      _logger = logger;

      context.Database.EnsureCreated();
    }
    public async Task<User> Authenticate(string username, string password)
    {
      try
      {
        var user = await _context.Users.Where(x => x.Username == username).SingleOrDefaultAsync();
        
        var passwordIsCorrect = bcrypt.Verify(password, user.Hash);

        if (!passwordIsCorrect || !user.EmailConfirmed.Value) throw new UserInvalidCredentialsException("User can not authenticate with the provided credentials");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("username",user.Username),
            new Claim("uuid", user.UserId.ToString()),
            new Claim("role", user.Role)
          }),
          Expires = DateTime.UtcNow.AddHours(9),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new User { Username = username, Email = user.Email, Token = tokenHandler.WriteToken(token) };

      }
      catch(InvalidOperationException)
      {
        throw new UserNotFoundException("User not found");
      }
    }

    public async Task<User> Register(User u)
    {
      try
      {
        _context.Users.Add(
          new UserModel
          {
            Email = u.Email,
            Username = u.Username,
            Hash = bcrypt.HashPassword(u.Password, workFactor: 12),
            Role = Roles.User,
            UserId = Guid.NewGuid(),
            EmailConfirmed = false,
            Token = randomTokenString()
          });
        await _context.SaveChangesAsync();
        u.Password = "";
        return u;
      }
      catch (DbUpdateException e)
      {
        _logger.LogCritical($"DB Exception on user register -> {e.Message}");
        return null;
      }
      catch (Exception e)
      {
        _logger.LogCritical($"Exception on user register -> {e.Message}");
        return null;
      }
    }

    public async Task<User> EmailVerifyAsync(string token)
    {
      try
      {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Token == token);

        if (user != null)
        {


          user.Token = null;
          user.EmailConfirmed = true;
          _context.Update(user);
          await _context.SaveChangesAsync();

          return new User
          {
            Email = user.Email,
            GUID = user.UserId,
            Username = user.Username
          };
        }
      }
      catch (InvalidOperationException)
      {
        return null;
      }

      return null;
    }

    public Task<User> RemoveUser(string username)
    {
      throw new NotImplementedException();
    }

    public Task<User> UpdatePassword(string newPassword1, string newPassword2)
    {
      throw new NotImplementedException();
    }

    public Task<User> UpdateUsername(string newUsername)
    {
      throw new NotImplementedException();
    }

    private static string randomTokenString()
    {
      using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
      var randomBytes = new byte[40];
      rngCryptoServiceProvider.GetBytes(randomBytes);
      // convert random bytes to hex string
      return BitConverter.ToString(randomBytes).Replace("-", "");
    }

  }

}