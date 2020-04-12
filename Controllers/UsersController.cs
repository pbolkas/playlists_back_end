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

namespace back_end.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController:ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    public UsersController(IUserService service, ILogger<UsersController> logger)
    {
      _userService = service;
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

        return Ok(new AuthenticateResponse{Token="blabla",Username="PBolkas"});
        
      }catch(Exception e)
      {
        _logger.LogCritical("Exception ",e.Message);
        return StatusCode(500);
      }
    }

    [AllowAnonymous]
    [HttpPost("subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] UserSubscribeRequest user)
    {
      return Ok("Created");
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