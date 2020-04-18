using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using back_end.Contracts.Requests.User;
using back_end.Helpers;
using back_end.Services;
using back_end.Contracts.Responses.User;
using back_end.Models;
using back_end.Entities;

namespace back_end.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController:ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly MySQLContext _context;
    public UsersController(IUserService service, MySQLContext context, ILogger<UsersController> logger)
    {
      _userService = service;
      _context = context;
      _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Login([FromBody]AuthenticateRequest UserCredentials)
    {
      
      try{

        var u = await _userService.Authenticate(UserCredentials.Username, UserCredentials.Password);

        return Ok(new AuthenticateResponse{Token="blabla",Username="PBolkas"});
        
      }catch(Exception e)
      {
        _logger.LogCritical($"Exception {e.Message}");
        return StatusCode(500);
      }
    }

    [AllowAnonymous]
    [HttpPost("subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] UserSubscribeRequest user)
    {
      try{
        var newUser = await _userService.Register(new User{Username = user.Username,Email=user.Email, Password= user.Password});
        
        return Ok("Created");        
      }catch(Exception e){
        _logger.LogCritical($"Exception {e.Message}");
        return StatusCode(500);
      }
    }

    [HttpPut("password")]
    public async Task<ActionResult> ChangePassword([FromBody] UserUpdatePasswordRequest user)
    {
      return Ok("Changed Password");
    }

    [HttpPut("username")]
    public async Task<ActionResult> ChangeUsername([FromBody] UserUpdateUsernameRequest user)
    {
      return Ok("Changed Username");
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveUser(string id)
    {
      return Ok("Removed user");
    }
    
  }
}